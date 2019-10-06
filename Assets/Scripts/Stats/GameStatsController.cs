using UnityEngine;
using Utils;
using System.Collections.Generic;

public class GameStatsController : MonoBehaviour
{
    public static int MAX_LIFES = 10;
    public static int MAX_BOTTLES = 7;
    public int currentLifes = 10;
    public int score = 0;
    public List<int> highScoreList = new List<int>();

    private bool _doNotDrainLife;
    private float _protectionTimeAfterTypo = 1f;
    private Scheduler _lifeProtectionScheduler;

    void OnEnable()
    {
        EventManager.StartListening(Events.BOTTLE_FAILURE, OnBottleFailure);
        EventManager.StartListening(Events.BOTTLE_SUCCESS, onBottleSuccess);
        EventManager.StartListening(Events.LOSE_LIFE, loseLife);
        EventManager.StartListening(Events.START_GAME, onGameStart);
    }

    void OnDisable()
    {
        EventManager.StopListening(Events.START_GAME, onGameStart);
        EventManager.StopListening(Events.BOTTLE_FAILURE, OnBottleFailure);
        EventManager.StopListening(Events.BOTTLE_SUCCESS, onBottleSuccess);
        EventManager.StopListening(Events.LOSE_LIFE, loseLife);
    }

    void Update()
    {
        if (_doNotDrainLife) _lifeProtectionScheduler.Update(Time.deltaTime);
    }

    private void OnBottleFailure(string typoPayload)
    {
        if (_doNotDrainLife) return;
        Debug.Log("PROTECTION START");

        _doNotDrainLife = true;
        _lifeProtectionScheduler = new Scheduler(_protectionTimeAfterTypo, () =>
        {
            Debug.Log("PROTECTION ENDE");
            _doNotDrainLife = false;
        });

        EventManager.TriggerEvent(Events.LOSE_LIFE, typoPayload);
    }
    
    private void loseLife(string typoPayload)
    {
        if(currentLifes > 1) {
            currentLifes--;
        } else {
            highScoreList.Add(this.score);
            EventManager.TriggerEvent(Events.GAME_OVER, score.ToString());
        }

        Debug.Log("looseLife");

    }

    public void addLife() {
        if (currentLifes < MAX_LIFES) {
            currentLifes++;
        }
    }

    private void onBottleSuccess(string bottleSuccessPayload) {
        score += 250;
    }
    
    private void onGameStart(string eventPayload) {
        this.score = 0;
        this.currentLifes = 10;
    }
}
