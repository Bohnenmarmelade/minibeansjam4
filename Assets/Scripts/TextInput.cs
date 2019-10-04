using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour
{
    private TypeableWord _typeableWord;

    public TypeableWord TypeableWord
    {
        get => _typeableWord;
        set
        {
            _typeableWord = value;
            textFieldTyped.text = _typeableWord.toBeTyped;
            Debug.Log("Got a new word: " + _typeableWord.fullWord);
        }
    }

    public Text textFieldTyped;

    // Start is called before the first frame update
    private void Start()
    {
        EventManager.StartListening(Events.KEY_DOWN, OnType);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnType(string keyDownPayload)
    {
        char typedCharacter = keyDownPayload[0];
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
