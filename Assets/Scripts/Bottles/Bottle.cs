using UnityEngine;

namespace Bottles
{
    public class Bottle : MonoBehaviour
    {
        public char firstLetter;

        public TypeableWord typeableWord;

        private FadingAnimation _fadingAnimation;

        public Sprite purpleBottle;
        public Sprite greenBottle;
        public Sprite blueBottle;

        private void Start()
        {
            gameObject.GetComponent<FadingAnimation>()
                .OnSpawn(() => gameObject.GetComponent<BottleText>().SetTextFieldsContent());

            PositionText();
            
            
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

        public void Init(string difficulty)
        {
            SetWord(Meds.getMed(difficulty));

            Sprite chosenSprite = null;
            switch (Random.Range(0, 3))
            {
                case 0:
                    chosenSprite = purpleBottle;
                    break;
                case 1:
                    chosenSprite = greenBottle;
                    break;
                case 2:
                    chosenSprite = blueBottle;
                    break;
            };

            gameObject.GetComponent<SpriteRenderer>().sprite = chosenSprite;
        }
    }
}