using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EffectSoundController : MonoBehaviour
{   
    public AudioClip keyPress;
    public AudioClip moveBottle;
    public AudioClip breakBottle1;
    public AudioClip breakBottle2;
    public AudioClip breakBottle3;

    private AudioSource source;

    void OnEnable()
    {
        EventManager.StartListening(Events.KEY_DOWN, onTyping);
        EventManager.StartListening(Events.BOTTLE_SUCCESS, onBottleSuccess);
        EventManager.StartListening(Events.BOTTLE_FAILURE, onBottleFailure);
    }

    void OnDisable()
    {
        EventManager.StopListening(Events.KEY_DOWN, onTyping);
        EventManager.StopListening(Events.BOTTLE_SUCCESS, onBottleSuccess);
        EventManager.StopListening(Events.BOTTLE_FAILURE, onBottleFailure);

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

    private void onBottleFailure(string eventPayload) {
        int random = (int) Random.Range(1f, 3f);
        AudioClip randomBottleBreak;

         switch (random){
            case 1:
                randomBottleBreak = breakBottle1;
                break;
            case 2:
                randomBottleBreak = breakBottle2;
                break;
            default:
                randomBottleBreak = breakBottle3;
                break;
         }
        source.PlayOneShot(randomBottleBreak);
    }
}
