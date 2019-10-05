using UnityEngine;
using UnityEngine.UI;

namespace Bottles
{
    public class BottleText : MonoBehaviour
    {
        public Text textField;

        public void SetTextFieldsContent()
        {
            textField.text =
                $"<color=#FFFFFF>{gameObject.GetComponent<Bottle>().typeableWord.fullWord[0].ToString()}</color>";
        }
    }
}