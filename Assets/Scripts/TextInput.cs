using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    private TypeableWord _typeableWord;
    public Text textFieldTyped;

    // Start is called before the first frame update
    private void Start()
    {
        EventManager.StartListening(Events.KEY_DOWN, OnType);
        _typeableWord = new TypeableWord("aeb");
        textFieldTyped.text = _typeableWord.toBeTyped;
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnType(char typedCharacter)
    {
        Debug.Log("TYPED CHAR");
        if (_typeableWord.type(typedCharacter))
            OnTypingCorrectly();
        else
        {
            OnTypingError();
        }
    }

    private void OnTypingError()
    {
        EventManager.TriggerEvent(Events.TYPO);
    }
    
    private void OnTypingCorrectly()
    {
        if (_typeableWord.toBeTyped.Length == 0)
            EventManager.StopListening(Events.KEY_DOWN, OnType);
        textFieldTyped.text = "<b>" + _typeableWord.succesfullyTyped + "</b>" + _typeableWord.toBeTyped;
    }
}
