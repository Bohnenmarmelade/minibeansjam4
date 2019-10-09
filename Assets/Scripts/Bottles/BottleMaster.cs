using System.Collections.Generic;
using Punishments;
using UnityEngine;
using Utils;

namespace Bottles
{
    public class BottleMaster : MonoBehaviour
    {
        public GameObject bottlePrefab;
        public TextInput textInput;

        private int _maxCountBottles = 100;
        private Dictionary<string, GameObject> _bottles;

        private string _currentDifficulty = Difficulty.EASY;

        private float _timeToSpawn = 3f;
        private Scheduler _spawnScheduler;

        private float _timeToIncreaseDifficulty = 10f;
        private Scheduler _difficultyScheduler;

        private float _zOffset;

        public List<char> currentFirstLetters;

        public ParticleSystem breakParticleSystem;

        private void Awake()
        {
            _bottles = new Dictionary<string, GameObject>();

            EventManager.StartListening(Events.KEY_DOWN, ActivateBottleFromKeyDown);
            EventManager.StartListening(Events.BOTTLE_SUCCESS, OnBottleSuccess);
            EventManager.StartListening(Events.BOTTLE_FAILURE, OnBottleFailure);
            EventManager.StartListening(Events.INCREASE_DIFFICULTY, payload => IncreaseDifficulty());
        }

        private void OnEnable()
        {
            _difficultyScheduler = new Scheduler(_timeToIncreaseDifficulty,
                () => EventManager.TriggerEvent(Events.INCREASE_DIFFICULTY));
            _spawnScheduler = new Scheduler(_timeToSpawn, SpawnBottle, true);
        }

        private void OnDisable()
        {
            EventManager.StopListening(Events.KEY_DOWN, ActivateBottleFromKeyDown);
            EventManager.StopListening(Events.BOTTLE_SUCCESS, OnBottleSuccess);
            EventManager.StopListening(Events.BOTTLE_FAILURE, OnBottleFailure);
            EventManager.StopListening(Events.INCREASE_DIFFICULTY, payload => IncreaseDifficulty());
        }

        private void Update()
        {
            _difficultyScheduler.Update(Time.deltaTime);
            _spawnScheduler.Update(Time.deltaTime);
        }

        private void SpawnBottle()
        {
            GameObject bottle = Instantiate(bottlePrefab, GetPosition(), Quaternion.identity);
            bottle.GetComponent<Bottle>().Init(_currentDifficulty, PunishmentType.GetRandomPunishment(), currentFirstLetters);
            bottle.transform.parent = gameObject.transform;
            RegisterBottle(bottle);
            EventManager.TriggerEvent(Events.BOTTLE_SPAWN);
        }

        void OnBottleFailure(string fullFailedWord)
        {
            if (_bottles.ContainsKey(fullFailedWord))
            {
                Bottle bottle = _bottles[fullFailedWord].GetComponent<Bottle>();
                bottle.StartPunishment();

                Vector3 breakParticlePosition = bottle.transform.position;
                breakParticlePosition.z = -9.5f;
                Instantiate(breakParticleSystem, breakParticlePosition, Quaternion.identity);

                DeregisterBottle(fullFailedWord, false);

                if (textInput && textInput.TypeableWord != null && fullFailedWord.Equals(textInput.TypeableWord.fullWord))
                {
                    textInput.TypeableWord = new TypeableWord("");
                }
            }
        }

        void OnBottleSuccess(string typeableWord)
        {
            DeregisterBottle(typeableWord, true);
            textInput.TypeableWord = new TypeableWord("");
        }
        
        void DeregisterBottle(string typeableWord, bool wasSuccess)
        {
            _bottles[typeableWord].GetComponent<Bottle>().OnCompletion(wasSuccess);
            _bottles.Remove(typeableWord);
            currentFirstLetters.Remove(typeableWord[0]);
        }

        void RegisterBottle(GameObject bottle)
        {
            if (_bottles.Count == _maxCountBottles) EventManager.TriggerEvent(Events.GAME_OVER);
            _bottles[bottle.gameObject.GetComponent<Bottle>().typeableWord.fullWord] = bottle;
        }

        void IncreaseDifficulty()
        {
            _currentDifficulty = Difficulty.GetNextHigherDifficulty(_currentDifficulty);
            Debug.Log($"Increased difficulty to '{_currentDifficulty}'");
        }

        void ActivateBottleFromKeyDown(string typedKey)
        {
            if (TextInputIsLocked()) return;

            foreach (KeyValuePair<string, GameObject> bottleEntry in _bottles)
            {
                if (BottleCanBeActivated(typedKey, bottleEntry.Key, bottleEntry.Value))
                {
                    SetAndUpdateActiveWord(bottleEntry.Value, typedKey);
                    return;
                }
            }
        }

        bool BottleCanBeActivated(string typedKey, string bottleEntryKey, GameObject bottleEntryValue)
        {
            return bottleEntryKey.ToLower()[0].Equals(typedKey[0]) &&
                   bottleEntryValue.GetComponent<FadingAnimation>().IsSpawned();
        }

        void SetAndUpdateActiveWord(GameObject bottle, string typedKey)
        {
            Bottle bottleComponent = bottle.gameObject.GetComponent<Bottle>();
            
            bottleComponent.Focus();
            
            var textInputTypeableWord = bottleComponent.typeableWord;
            textInputTypeableWord.type(typedKey[0]);

            textInput.TypeableWord = textInputTypeableWord;
        }

        bool TextInputIsLocked()
        {
            var textInputTypeableWord = textInput.TypeableWord;
            if (textInputTypeableWord == null)
                return false;
            if (textInput.TypeableWord.fullWord.Equals(""))
                return false;
            
            return true;
        }

        private Vector3 GetPosition()
        {
            float randX = Mathf.Round(Random.Range(-2.7625f, 2.7625f) * 16) / 16;
            return new Vector3(randX, -0.8125f, 2.792969f + (++_zOffset));
        }
    }
}