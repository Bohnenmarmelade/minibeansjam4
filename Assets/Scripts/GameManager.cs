using UnityEngine; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.StartListening(Events.GAME_OVER, gameOver);
        EventManager.StartListening(Events.START_GAME, onStartGame);
        EventManager.StartListening(Events.SHOW_CREDITS, onShowCredits);
        EventManager.StartListening(Events.SHOW_HIGHSCORE, onShowHighscore);
        SceneManager.LoadScene("TitleScreen");
    }

    void OnDisable()
    {
        EventManager.StopListening(Events.GAME_OVER, gameOver);
    }

    public void onShowCredits(string _)
    {
        EventManager.StartListening(Events.SHOW_TITLE, onShowTitleScreen);
        SceneManager.LoadScene("CreditsScene");
    }

    public void onShowTitleScreen(string _)
    {
        EventManager.StopListening(Events.SHOW_TITLE, onShowTitleScreen);
        SceneManager.LoadScene("TitleScreen");
    }

    private void onStartGame(string eventPayload){
        SceneManager.LoadScene("PlayScene");
    }
    private void gameOver(string gameOverPayload) {
        Debug.Log("GameOver Dude!!!");
        SceneManager.LoadScene("GameOverScene");
    }

    private void onShowHighscore(string eventPayload) {
        SceneManager.LoadScene("HighscoreScene");
    }
}
