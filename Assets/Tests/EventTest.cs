using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTest : MonoBehaviour
{
    private UnityAction someListener;

    void OnEnable()
    {
        EventManager.StartListening("keyDown", SomeFunction);
    }

    void OnDisable()
    {
        EventManager.StopListening("keyDown", SomeFunction);
    }


    void SomeFunction(char c)
    {
        Debug.Log("Key was pressed: " + c);
    }

}
