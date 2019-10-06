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
        public Sprite greenBottleSuccess;
        public Sprite purpleBottleSuccess;
        public Sprite blueBottleSuccess;

        private GameObject _punishment;
        public GameObject poisonPunishment;
        public GameObject shakePunishment;
        public GameObject sleepPunishment;

        private Scheduler _destroyScheduler;

        private List<Bottle> _collidingBottleCandidates = new List<Bottle>();
        private Sprite _finishedSprite;
        
        public Material outlineMaterial;
        private Material _defaultMaterial;

        private void Start()
        {
            _fadingAnimation = gameObject.GetComponent<FadingAnimation>();
            _fadingAnimation.OnSpawn(OnFinishedSpawn);

            PositionText();

            _defaultMaterial = gameObject.GetComponent<SpriteRenderer>().material;
        }

        private void Update()
        {
            _destroyScheduler?.Update(Time.deltaTime);
        }

        public void OnCompletion(bool wasSuccess)
        {
            if (wasSuccess)
            {
                _destroyScheduler = new Scheduler(0.5f, () => Destroy(gameObject));
                gameObject.GetComponent<SpriteRenderer>().sprite = _finishedSprite;
            }
            else
            {
                Destroy(gameObject);
            }

            gameObject.GetComponent<SpriteRenderer>().material = _defaultMaterial;
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

        public void Focus()
        {
            gameObject.GetComponent<SpriteRenderer>().material = outlineMaterial;
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
                    _finishedSprite = greenBottleSuccess;
                    _punishment = poisonPunishment;
                    break;
                case PunishmentType.SHAKE:
                    chosenSprite = purpleBottle;
                    _finishedSprite = purpleBottleSuccess;
                    _punishment = shakePunishment;
                    break;
                case PunishmentType.SLEEP:
                    chosenSprite = blueBottle;
                    _finishedSprite = blueBottleSuccess;
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