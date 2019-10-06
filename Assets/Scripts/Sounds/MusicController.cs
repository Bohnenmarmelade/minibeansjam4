using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class MusicController : MonoBehaviour
{   
    public AudioClip intro;
    public AudioClip loop1;
    public AudioClip loop2;
    public AudioClip outro;
    public AudioSource mainSource;
    public AudioClip clipToPlay;
    private int toggle = 0;
    private bool fadeIn = false;
    private bool fadeOut = false;

    private float FadeTime = 1f;
    private float startVolume;
    void Start () {

    }
    void OnEnable()
    {
        clipToPlay = intro;
        startVolume = mainSource.volume;
        mainSource.volume = 0.0f;
        fadeIn = true;

        EventManager.StartListening(Events.START_GAME, onStartGame);
        EventManager.StartListening(Events.GAME_OVER, onGameOver);
        EventManager.StartListening(Events.INCREASE_DIFFICULTY, onIncreaseDifficulty);        
    }

    void OnDisable()
    {
        EventManager.StopListening(Events.START_GAME, onStartGame);
        EventManager.StopListening(Events.GAME_OVER, onGameOver);
        EventManager.StopListening(Events.INCREASE_DIFFICULTY, onIncreaseDifficulty);        
    }
    void Update () {
         if (fadeOut) {
              mainSource.volume -= startVolume * Time.deltaTime / FadeTime;
              if (mainSource.volume < 0.1) {
                  fadeOut = false;
                  fadeIn = true;
                  mainSource.clip = clipToPlay;
                  mainSource.Play();
              }
         } else if (fadeIn) {
              mainSource.volume += startVolume * Time.deltaTime / FadeTime;
              if (mainSource.volume > 0.5) {
                  fadeIn = false;
              }
         }
    }

    void onStartGame(string eventPayload) {
        clipToPlay = loop1;
        fadeOut = true;
    }

    void onIncreaseDifficulty(string eventPayload) {
        clipToPlay = loop2;
        fadeOut = true;
    }

    void onGameOver(string eventPayload) {
        clipToPlay = outro;
        fadeOut = true;
    }
}
