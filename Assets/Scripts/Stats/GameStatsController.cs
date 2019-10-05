using UnityEngine;
using Utils;
using System.Collections.Generic;

public class GameStatsController : MonoBehaviour
{

    public static int MAX_LIFES = 10;
    public static int MAX_BOTTLES = 7;
    public int currentLifes = 10;
    public int currentBottles = 0;
    public int score = 0;
    public List<int> highScoreList = new List<int>();


    void OnEnable()
    {
        EventManager.StartListening(Events.BOTTLE_FAILURE, loseLife);
        EventManager.StartListening(Events.BOTTLE_SUCCESS, onBottleSuccess);
        EventManager.StartListening(Events.START_GAME, onGameStart);
        EventManager.StartListening(Events.BOTTLE_SPAWN, onBottleSpawn);
    }

    void OnDisable()
    {
        EventManager.StopListening(Events.START_GAME, onGameStart);
        EventManager.StopListening(Events.BOTTLE_FAILURE, loseLife);
        EventManager.StopListening(Events.BOTTLE_SUCCESS, onBottleSuccess);
        EventManager.StopListening(Events.BOTTLE_SPAWN, onBottleSpawn);
    }

    private void loseLife(string typoPayload) {
        if(currentLifes > 1) {
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
        this.currentBottles--;
    }
    
    private void onGameStart(string eventPayload) {
        this.score = 0;
        this.currentLifes = 10;
    }

    private void onBottleSpawn(string eventPayload) {
        this.currentBottles++;
        if (this.currentBottles > MAX_BOTTLES) {
            EventManager.TriggerEvent(Events.GAME_OVER, "bottles");
        }
    }
}
