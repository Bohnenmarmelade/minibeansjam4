using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{

    public static int MAX_LIFES = 10;
    public int currentLifes = 10;


    void OnEnable()
    {
        EventManager.StartListening(Events.TYPO, looseLife);
    }

    void OnDisable()
    {
        EventManager.StopListening(Events.TYPO, looseLife);
    }

    private void looseLife(string typoPayload) {
        if(currentLifes > 1){
            currentLifes--;
        }else{
            EventManager.TriggerEvent(Events.GAME_OVER, "");
        }

        Debug.Log("looseLife");

    }

    public void addLife() {
        if (currentLifes < MAX_LIFES) {
            currentLifes++;
        }
    }

    public int getCurrentLifes()
    {
        return currentLifes;
    }
}
