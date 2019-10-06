using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Bottles
{
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
                
                if (!_typeableWord.fullWord.Equals(""))
                {
                    EventManager.StartListening(Events.KEY_DOWN, OnType);
                }
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
                Debug.Log($"FAYUUUUL {typedCharacter}, {_typeableWord.fullWord}, {_typeableWord.toBeTyped}");
                OnTypingError(typedCharacter);
            }
        }

        private void OnTypingError(char typo)
        {
            EventManager.StopListening(Events.KEY_DOWN, OnType);
            EventManager.TriggerEvent(Events.BOTTLE_FAILURE, _typeableWord.fullWord);
        }

        private void OnTypingCorrectly()
        {
            if (_typeableWord.toBeTyped.Length == 0)
            {
                EventManager.StopListening(Events.KEY_DOWN, OnType);
                EventManager.TriggerEvent(Events.BOTTLE_SUCCESS, _typeableWord.fullWord);
            }

            SetTextFieldsContent();
        }

        private void SetTextFieldsContent()
        {
            textFieldTyped.text = "<b>" + _typeableWord.succesfullyTyped.ToUpper() + "</b>" + "<color=#D3D3D3>" +
                                  _typeableWord.toBeTyped.ToUpper() + "</color>";
        }
    }
}