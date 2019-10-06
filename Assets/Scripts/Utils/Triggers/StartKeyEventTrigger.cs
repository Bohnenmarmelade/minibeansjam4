using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class StartKeyEventTrigger : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            EventManager.TriggerEvent(Events.START_GAME, "");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            EventManager.TriggerEvent(Events.SHOW_CREDITS, "");
        }
    }
}
