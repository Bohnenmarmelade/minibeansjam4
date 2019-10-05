using System;
using UnityEngine;
using UnityEngine.Events;
using Utils;

public class Bottle : MonoBehaviour
{
    public char firstLetter;
    public TextInput textInput;

    private void SetWord(string word)
    {
        Debug.Log($"Word is set: '${word}'");
        textInput.TypeableWord = new TypeableWord(word);
        firstLetter = textInput.TypeableWord.fullWord[0];
    }

    public void InitWordByDifficulty(string difficulty)
    {
        SetWord(Meds.getMed(difficulty));
    }
}
