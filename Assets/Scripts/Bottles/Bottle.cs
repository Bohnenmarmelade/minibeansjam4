using UnityEngine;

namespace Bottles
{
    public class Bottle : MonoBehaviour
    {
        public char firstLetter;

        public TypeableWord typeableWord;

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