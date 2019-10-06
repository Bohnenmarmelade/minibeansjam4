using System;
using System.Collections.Generic;
using Punishments;
using UnityEngine;
using Utils;

namespace Bottles
{
    public class Bottle : MonoBehaviour
    {
        public TypeableWord typeableWord;

        private FadingAnimation _fadingAnimation;
        private bool _willColideWithAnotherBottle;

        public Sprite greenBottle;
        public Sprite purpleBottle;
        public Sprite blueBottle;

        private GameObject _punishment;
        public GameObject poisonPunishment;
        public GameObject shakePunishment;
        public GameObject sleepPunishment;

        private List<Bottle> _collidingBottleCandidates = new List<Bottle>();

        private void Start()
        {
            _fadingAnimation = gameObject.GetComponent<FadingAnimation>();
            _fadingAnimation.OnSpawn(OnFinishedSpawn);

            PositionText();
        }

        private void OnFinishedSpawn()
        {
            gameObject.GetComponent<BottleText>().SetTextFieldsContent();
            foreach (var collidingBottle in _collidingBottleCandidates)
            {
                if (collidingBottle != null)
                    EventManager.TriggerEvent(Events.BOTTLE_FAILURE, collidingBottle.typeableWord.fullWord);
            }
        }

        private void PositionText()
        {
            var position = gameObject.transform.position;
            var transformPosition = new Vector3(position.x, position.y + 3f, 0f);

            gameObject.GetComponentInChildren<RectTransform>().transform.position = transformPosition;
        }

        private void SetWord(string word)
        {
            Debug.Log($"Word is set: '{word}'");
            typeableWord = new TypeableWord(word);
        }

        public void Init(string currentDifficulty, string punishmentType, List<char> currentFirstLetters)
        {   
            InitWord(currentDifficulty, currentFirstLetters);

            InitPunishmentAndSprite(punishmentType);
            
            IntColliderFromSprite();
        }

        private void InitWord(string currentDifficulty, List<char> currentFirstLetters)
        {
            string newWord = "";
            bool wordIsValid = false;
            while (!wordIsValid) {
                newWord = Meds.getMed(currentDifficulty);
                if (!currentFirstLetters.Contains(newWord[0])) {
                    wordIsValid = true;
                    currentFirstLetters.Add(newWord[0]);
                }
            }

            SetWord(newWord);
        }

        private void InitPunishmentAndSprite(string punishmentType)
        {
            Sprite chosenSprite;
            switch (punishmentType)
            {
                default:
                    chosenSprite = greenBottle;
                    _punishment = poisonPunishment;
                    break;
                case PunishmentType.SHAKE:
                    chosenSprite = purpleBottle;
                    _punishment = shakePunishment;
                    break;
                case PunishmentType.SLEEP:
                    chosenSprite = blueBottle;
                    _punishment = sleepPunishment;
                    break;
            }
            gameObject.GetComponent<SpriteRenderer>().sprite = chosenSprite;
        }
        
        private void IntColliderFromSprite()
        {
            Sprite sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            BoxCollider2D bo = gameObject.AddComponent<BoxCollider2D>();
            bo.isTrigger = true;
            bo.size = new Vector2(
                sprite.bounds.size.x -
                (sprite.border.x + sprite.border.z) / sprite.pixelsPerUnit,
                sprite.bounds.size.y);

            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            rb.isKinematic = true;
        }

        public void StartPunishment()
        {
            Debug.Log("[Bottle] try to start punishment");
            _punishment.GetComponent<IPunishment>().startPunishment(transform.position);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_fadingAnimation.IsSpawned())
            {
                _collidingBottleCandidates.Add(other.GetComponent<Bottle>());
            }
        }
    }
}