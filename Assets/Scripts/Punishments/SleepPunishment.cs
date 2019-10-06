using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepPunishment : MonoBehaviour, IPunishment
{

    public GameObject Blink;

    public void startPunishment()
    {
        startPunishment(new Vector3(0, 0, 0));
    }

    public void startPunishment(Vector3 position)
    {
        Instantiate(Blink, position, Quaternion.identity);
    }
}
