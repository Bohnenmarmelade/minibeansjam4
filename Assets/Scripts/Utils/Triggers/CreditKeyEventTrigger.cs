using Utils;
using UnityEngine;

public class CreditKeyEventTrigger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.TriggerEvent(Events.SHOW_TITLE, "");
        }
    }
}
