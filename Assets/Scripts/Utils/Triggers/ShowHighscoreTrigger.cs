using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ShowHighscoreTrigger : MonoBehaviour
{
    void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                EventManager.TriggerEvent(Events.SHOW_HIGHSCORE, "");
            }
        }
}
