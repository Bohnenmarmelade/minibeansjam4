using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.StartListening("gameOver", gameOver);
    }

    void OnDisable()
    {
        EventManager.StopListening("gameOver", gameOver);
    }

    private void gameOver(char c) {
        Debug.Log("GameOver Dude!!!");
    }
}
