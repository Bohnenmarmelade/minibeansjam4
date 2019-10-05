using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{

    public static int MAX_LIFES = 10;
    public int currentLifes = 10;
    public int score = 0;


    void OnEnable()
    {
        EventManager.StartListening(Events.TYPO, looseLife);
        EventManager.StartListening(Events.WORD_SUCCESS, wordSuccess);
    }

    void OnDisable()
    {
        EventManager.StopListening(Events.TYPO, looseLife);
        EventManager.StopListening(Events.WORD_SUCCESS, wordSuccess);
    }

    private void looseLife(string typoPayload) {
        if(currentLifes > 1){
            currentLifes--;
        }else{
            EventManager.TriggerEvent(Events.GAME_OVER, "" + score);
        }

        Debug.Log("looseLife");

    }

    public void addLife() {
        if (currentLifes < MAX_LIFES) {
            currentLifes++;
        }
    }

    private void wordSuccess(string wordSuccessPayload) {
        score += 1;
    }
}
