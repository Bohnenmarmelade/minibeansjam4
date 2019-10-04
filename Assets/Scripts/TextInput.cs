using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    private TypeableWord _typeableWord;
    public Text textFieldTyped;

    private bool _locked = false;
    
    public TypeableWord TypeableWord
    {
        set => _typeableWord = value;
    }

    private static readonly KeyCode[] allowedKeys = {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E
    };

    // Start is called before the first frame update
    private void Start()
    {
        _typeableWord = new TypeableWord("aeb");
        OnTypingCorrectly();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_locked) return;
        
        foreach(KeyCode kcode in allowedKeys)
        {
            if (Input.GetKeyDown(kcode)) {
                var isCorrectChar = _typeableWord.type(kcode.ToString().ToLower()[0]);
                if (isCorrectChar)
                {
                    OnTypingCorrectly();
                }
                else
                {
                    OnTypingError();
                }
            }
        }
    }

    private void OnTypingError()
    {
        textFieldTyped.text = "YOU FAILED";
        _locked = true;
    }
    
    private void OnTypingCorrectly()
    {
        textFieldTyped.text = "<b>" + _typeableWord.succesfullyTyped + "</b>" + _typeableWord.toBeTyped;
    }
}
