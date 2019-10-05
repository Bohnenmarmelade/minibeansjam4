using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class SoundController : MonoBehaviour
{   
    public AudioClip keyPress;
    private AudioSource source;

    void OnEnable()
    {
        EventManager.StartListening(Events.KEY_DOWN, onTyping);
    }

    void OnDisable()
    {
        EventManager.StartListening(Events.KEY_DOWN, onTyping);
    }

    void Awake () {
        source = GetComponent<AudioSource>();
    }

    void onTyping(string typingPayload) {
        source.PlayOneShot(keyPress);
    }
}
