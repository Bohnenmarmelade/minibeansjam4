using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonPunishment : MonoBehaviour, IPunishment
{

    public ParticleSystem swirl;
    public ParticleSystem clouds;

    public void startPunishment()
    {
        Debug.Log("startPunishment without a position is risky for PoisonPunishment, using: " + transform.position);
        startPunishment(transform.position);
    }

    public void startPunishment(Vector3 position)
    {
        Debug.Log("starting punishment");
        position.z -= 3;
        Instantiate(swirl, position, Quaternion.identity);
        Instantiate(clouds, position, Quaternion.identity);
    }
}
