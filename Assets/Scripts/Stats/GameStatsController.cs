using UnityEngine;
using Utils;
using System.Collections.Generic;

public class GameStatsController : MonoBehaviour
{

    public static int MAX_LIFES = 10;
    public int currentLifes = 10;
    public int score = 0;
    public List<int> highScoreList = new List<int>();


    void OnEnable()
    {
        EventManager.StartListening(Events.TYPO, looseLife);
        EventManager.StartListening(Events.BOTTLE_SUCCES, onBottleSuccess);
    }

    void OnDisable()
    {
        EventManager.StopListening(Events.TYPO, looseLife);
        EventManager.StopListening(Events.BOTTLE_SUCCES, onBottleSuccess);
    }

    private void looseLife(string typoPayload) {
        if(currentLifes > 1){
            currentLifes--;
        }else{
            highScoreList.Add(this.score);
            EventManager.TriggerEvent(Events.GAME_OVER, "" + this.score);
        }

        Debug.Log("looseLife");

    }

    public void addLife() {
        if (currentLifes < MAX_LIFES) {
            currentLifes++;
        }
    }

    private void onBottleSuccess(string bottleSuccessPayload) {
        score += 1;
    }
}
