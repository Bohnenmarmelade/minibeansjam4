using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeableWord
{
    public string fullWord;

    public string succesfullyTyped;
    public string toBeTyped;

    public TypeableWord(string word)
    {
        this.fullWord = word;
        this.toBeTyped = word;
        this.succesfullyTyped = "";
    }

    public bool type(char typedCharacter)
    {
        char character = Char.ToLower(typedCharacter);
        if (toBeTyped.ToLower()[0].Equals(character))
        {
            succesfullyTyped += toBeTyped[0];
            toBeTyped = toBeTyped.Substring(1);
            return true;
        }
        else
        {
            return false;
        }
    }
}