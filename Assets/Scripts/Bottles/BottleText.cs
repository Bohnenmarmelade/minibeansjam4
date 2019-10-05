using UnityEngine;
using UnityEngine.UI;

namespace Bottles
{
    public class BottleText : MonoBehaviour
    {
        public Text textField;

        public void SetTextFieldsContent()
        {
            textField.text = gameObject/*.transform.parent*/
                .GetComponent<Bottle>()
                .typeableWord
                .fullWord[0]
                .ToString();
        }
    }
}
