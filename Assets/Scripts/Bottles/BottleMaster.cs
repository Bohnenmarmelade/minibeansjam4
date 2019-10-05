using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Bottles
{
    public class BottleMaster : MonoBehaviour
    {
        public GameObject bottlePrefab;
        public TextInput textInput;

        private Dictionary<string, GameObject> _bottles;

        private List<Vector3> _positions;

        private string _currentDifficulty = Difficulty.EASY;

        private float _timeToSpawn = 3f;
        private Scheduler _spawnScheduler;

        private float _timeToIncreaseDifficulty = 10f;
        private Scheduler _difficultyScheduler;

        private void Awake()
        {
            _positions = new List<Vector3>();
            _positions.Add(new Vector3(Random.Range(-10f, 10f), -4f, 2.792969f));
            _positions.Add(new Vector3(Random.Range(-10f, 10f), -4f, 2.792969f));
            _positions.Add(new Vector3(Random.Range(-10f, 10f), -4f, 2.792969f));
            _positions.Add(new Vector3(Random.Range(-10f, 10f), -4f, 2.792969f));

            _bottles = new Dictionary<string, GameObject>();

            EventManager.StartListening(Events.KEY_DOWN, ActiveBottleFromKeyDown);
            EventManager.StartListening(Events.BOTTLE_SUCCES, DeregisterBottle);
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
            EventManager.StopListening(Events.BOTTLE_SUCCES, DeregisterBottle);
            EventManager.StopListening(Events.INCREASE_DIFFICULTY, payload => IncreaseDifficulty());
        }

        private void Update()
        {
            _difficultyScheduler.Update(Time.deltaTime);
            _spawnScheduler.Update(Time.deltaTime);
        }

        private void SpawnBottle()
        {
            GameObject bottle = Instantiate(bottlePrefab, _positions[0], Quaternion.identity);
            bottle.GetComponent<Bottle>().InitWordByDifficulty(_currentDifficulty);
            bottle.transform.parent = gameObject.transform;

            RegisterBottle(bottle);
        }

        void DeregisterBottle(string typeableWord)
        {
            _positions.Add(_bottles[typeableWord].transform.position);
            Destroy(_bottles[typeableWord]);
            _bottles.Remove(typeableWord);

            textInput.TypeableWord = new TypeableWord("");
        }

        void RegisterBottle(GameObject bottle)
        {
            _bottles[bottle.gameObject.GetComponent<Bottle>().typeableWord.fullWord] = bottle;
            _positions.RemoveAt(0);
        }

        void IncreaseDifficulty()
        {
            _currentDifficulty = Difficulty.GetNextHigherDifficulty(_currentDifficulty);
            Debug.Log($"Increased difficulty to '{_currentDifficulty}'");
        }

        void ActiveBottleFromKeyDown(string typedKey)
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
            var textInputTypeableWord = bottle.gameObject.GetComponent<Bottle>().typeableWord;
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
    }
}