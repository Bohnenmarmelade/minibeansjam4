using UnityEngine;
using Utils;

public class GameStatsController : MonoBehaviour
{

    public static int MAX_LIFES = 10;
    public int currentLifes = 10;
    public int score = 0;


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
            EventManager.TriggerEvent(Events.GAME_OVER, "" + score);
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
