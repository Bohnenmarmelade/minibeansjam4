using UnityEngine;

public class Bottle : MonoBehaviour
{
    public char firstLetter;
    public TextInput textInput;

    // Start is called before the first frame update
    void OnEnable()
    {
        SetWord(Meds.getMed(Difficulty.EASY).ToLower());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetWord(string word)
    {
        textInput.TypeableWord = new TypeableWord(word);
        firstLetter = textInput.TypeableWord.fullWord[0];
    }
}
