using UnityEngine;

namespace Bottles
{
    public class Bottle : MonoBehaviour
    {


        public GameObject punishment;
        public char firstLetter;

        public TypeableWord typeableWord;

        private FadingAnimation _fadingAnimation;
        private BottleText _bottleText;

        private void Start()
        {
            _bottleText = gameObject.GetComponent<BottleText>();
            gameObject.GetComponent<FadingAnimation>().OnSpawn(() => _bottleText.SetTextFieldsContent());

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
            gameObject.GetComponentInChildren<BottleText>().SetTextFieldsContent();
        }

        public void InitWordByDifficulty(string difficulty)
        {
            SetWord(Meds.getMed(difficulty));
        }
    }
}