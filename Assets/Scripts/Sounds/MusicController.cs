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
    //private AudioSource introAudioSource;
    //private AudioSource loopAudioSource;
    public AudioSource[] audioSourceArray;
    public AudioClip clipToPlay;
    private double nextStartTime = 0.0;
    private int toggle = 0;
    void Start () {
        clipToPlay = intro;
    }
    void OnEnable()
    {
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

    if(AudioSettings.dspTime > nextStartTime - 1) {

        // Loads the next Clip to play and schedules when it will start
        audioSourceArray[toggle].clip = clipToPlay;
        audioSourceArray[toggle].PlayScheduled(nextStartTime);

        // Checks how long the Clip will last and updates the Next Start Time with a new value
        double duration = (double)clipToPlay.samples / clipToPlay.frequency;
        nextStartTime = nextStartTime + duration;

        toggle = 1 - toggle;

        }
    }

    void onStartGame(string eventPayload) {
        clipToPlay = loop1;
    }

    void onIncreaseDifficulty(string eventPayload) {
        clipToPlay = loop2;
    }

    void onGameOver(string eventPayload) {
        clipToPlay = outro;
    }
}
