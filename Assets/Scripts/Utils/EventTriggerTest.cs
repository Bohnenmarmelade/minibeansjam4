using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            Debug.Log("trigger q");
            EventManager.TriggerEvent("typo",'q');
        }

    }

}
