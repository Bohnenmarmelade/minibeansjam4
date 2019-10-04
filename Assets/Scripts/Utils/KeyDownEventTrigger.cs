using System;
using UnityEngine;

namespace Utils
{
    public class KeyDownEventTrigger : MonoBehaviour
    {
        // Start is called before the first frame update
        void Update()
        {
            if (Input.anyKey && Input.inputString.Length > 0)
            {
                char key = Input.inputString.ToLower()[0];
                if (Char.IsLetter(key))
                {
                    Debug.Log("triggerKey " + Input.inputString);
                    EventManager.TriggerEvent(Events.KEY_DOWN, key);
                }
            }
        }
    }
}