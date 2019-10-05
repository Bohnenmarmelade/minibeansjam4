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
        newWord();
    }

    private void newWord()
    {
        _typeableWord = new TypeableWord(Meds.getMed(Difficulty.NINETOUSANDANDONE).ToUpper());
        textFieldTyped.text = "<color=#D3D3D3>" + _typeableWord.toBeTyped + "</color>";
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
        {
            //EventManager.StopListening(Events.KEY_DOWN, OnType);
            newWord();
        }
            
        textFieldTyped.text = "<b>" + _typeableWord.succesfullyTyped + "</b>" + "<color=#D3D3D3>" + _typeableWord.toBeTyped+ "</color>";
    }
}
