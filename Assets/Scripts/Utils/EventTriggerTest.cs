using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerTest : MonoBehaviour
{

    private static readonly KeyCode[] allowedKeys = {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E
    };
    // Start is called before the first frame update
    void Update() {
        if (Input.anyKey && Input.inputString.Length > 0){
            Debug.Log("inputstring: " + Input.inputString);
            char key = Input.inputString.ToLower()[0];
            if (Char.IsLetter(key)){
                Debug.Log("triggerKey " + Input.inputString);
                EventManager.TriggerEvent("typo", key);
            }

        }

    }

}
