using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEventTranslator : MonoBehaviour
{
    private static readonly KeyCode[] allowedKeys = {
        KeyCode.A, 
        KeyCode.B, 
        KeyCode.C, 
        KeyCode.D, 
        KeyCode.E,
        KeyCode.F,
        KeyCode.G,
        KeyCode.H,
        KeyCode.I,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
        KeyCode.M,
        KeyCode.N,
        KeyCode.O,
        KeyCode.P,
        KeyCode.Q,
        KeyCode.R,
        KeyCode.S,
        KeyCode.T,
        KeyCode.U,
        KeyCode.V,
        KeyCode.W,
        KeyCode.X,
        KeyCode.Y,
        KeyCode.Z
    };

     void Update()
    {   
        foreach(KeyCode kcode in allowedKeys)
        {
            if (Input.GetKeyDown(kcode))
            {
                Debug.Log("trigger " + kcode);
                EventManager.TriggerEvent("keyDown",kcode);
            }
        }

    }
}
