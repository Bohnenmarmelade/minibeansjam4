using UnityEngine;
using UnityEngine.UI;
using Utils;

public class TextInput : MonoBehaviour
{
    private TypeableWord _typeableWord;

    public TypeableWord TypeableWord
    {
        get => _typeableWord;
        set
        {
            _typeableWord = value;
            SetTextFieldsContent();
            EventManager.StartListening(Events.KEY_DOWN, OnType);
        }
    }

    public Text textFieldTyped;

    private void OnType(string keyDownPayload)
    {
        char typedCharacter = keyDownPayload[0];
        if (_typeableWord.type(typedCharacter))
            OnTypingCorrectly();
        else
        {
            OnTypingError(typedCharacter);
        }
    }

    private void OnTypingError(char typo)
    {
        EventManager.TriggerEvent(Events.TYPO, typo.ToString());
        EventManager.TriggerEvent(Events.SHAKE, "10");
    }
    
    private void OnTypingCorrectly()
    {
        if (_typeableWord.toBeTyped.Length == 0)
        {
            EventManager.StopListening(Events.KEY_DOWN, OnType);
            EventManager.TriggerEvent(Events.BOTTLE_SUCCES, _typeableWord.fullWord);
        }

        SetTextFieldsContent();
    }

    private void SetTextFieldsContent()
    {
        textFieldTyped.text = "<b>" + _typeableWord.succesfullyTyped + "</b>" + "<color=#D3D3D3>" +
               _typeableWord.toBeTyped + "</color>";
    }
}
