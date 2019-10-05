using System;
using System.Collections.Generic;
using Punishments;
using UnityEngine;
using Utils;

namespace Bottles
{
    public class Bottle : MonoBehaviour
    {
        public GameObject punishment;
        public char firstLetter;

        public TypeableWord typeableWord;

        private FadingAnimation _fadingAnimation;
        private bool _willColideWithAnotherBottle;

        public Sprite purpleBottle;
        public Sprite greenBottle;
        public Sprite blueBottle;

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
            firstLetter = typeableWord.fullWord[0];
        }

        public void Init(string currentDifficulty, string punishmentType)
        {
            SetWord(Meds.getMed(currentDifficulty));

            Sprite chosenSprite;
            switch (punishmentType)
            {
                default:
                    chosenSprite = purpleBottle;
                    break;
                case PunishmentType.SHAKE:
                    chosenSprite = greenBottle;
                    break;
                case PunishmentType.SLEEP:
                    chosenSprite = blueBottle;
                    break;
            }

            gameObject.GetComponent<SpriteRenderer>().sprite = chosenSprite;

            BoxCollider2D bo = gameObject.AddComponent<BoxCollider2D>();
            bo.isTrigger = true;
            bo.size = new Vector2(
                chosenSprite.bounds.size.x -
                (chosenSprite.border.x + chosenSprite.border.z) / chosenSprite.pixelsPerUnit,
                chosenSprite.bounds.size.y);

            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            rb.isKinematic = true;
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