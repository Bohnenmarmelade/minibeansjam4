using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class TypingSoundController : MonoBehaviour
{   
    public AudioClip keyPress;
    public AudioClip moveBottle;
    private AudioSource source;

    void OnEnable()
    {
        EventManager.StartListening(Events.KEY_DOWN, onTyping);
        EventManager.StartListening(Events.BOTTLE_SUCCES, onBottleSuccess);
    }

    void OnDisable()
    {
        EventManager.StopListening(Events.KEY_DOWN, onTyping);
        EventManager.StopListening(Events.BOTTLE_SUCCES, onBottleSuccess);
    }

    void Awake () {
        source = GetComponent<AudioSource>();
    }

    void onTyping(string typingPayload) {
        source.PlayOneShot(keyPress);
    }

    private void onBottleSuccess(string eventPayload) {
        source.PlayOneShot(moveBottle);
    }
}
