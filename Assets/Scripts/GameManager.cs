using UnityEngine; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.StartListening(Events.GAME_OVER, gameOver);
        SceneManager.LoadScene("PlayScene");
    }

    void OnDisable()
    {
        EventManager.StopListening(Events.GAME_OVER, gameOver);
    }

    private void gameOver(string gameOverPayload) {
        Debug.Log("GameOver Dude!!!");
        SceneManager.LoadScene("ScoreScene");
        //GameObject[] test = GameObject.FindGameObjectsWithTag("ScoreText");
        //Debug.Log("Found :" + test[0].ToString());
    }
}
