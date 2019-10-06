using System;

public class TypeableWord
{
    public string fullWord;

    public string succesfullyTyped;
    public string toBeTyped;

    public TypeableWord(string word)
    {
        fullWord = word;
        toBeTyped = word;
        succesfullyTyped = "";
    }

    public bool type(char typedCharacter)
    {
        if (toBeTyped.Equals("")) return true;
        
        char character = Char.ToLower(typedCharacter);
        if (toBeTyped.ToLower()[0].Equals(character))
        {
            succesfullyTyped += toBeTyped[0];
            toBeTyped = toBeTyped.Substring(1);
            return true;
        }

        return false;
    }
}