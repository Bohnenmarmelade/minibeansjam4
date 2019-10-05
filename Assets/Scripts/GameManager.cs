using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text finalScore;

    void OnEnable()
    {
        EventManager.StartListening(Events.GAME_OVER, gameOver);
    }

    void OnDisable()
    {
        EventManager.StopListening(Events.GAME_OVER, gameOver);
    }

    private void gameOver(string gameOverPayload) {
        Debug.Log("GameOver Dude!!!");
        SceneManager.LoadScene("ScoreScene");
        finalScore.text = gameOverPayload;
    }
}
