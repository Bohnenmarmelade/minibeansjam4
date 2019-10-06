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
    public AudioClip mitP;
    public AudioClip mitA;
    public AudioClip ohMan;
    public AudioClip oops;
    public AudioClip passAuf1;
    public AudioClip passAuf2;
    public AudioClip verdammt;
    public AudioClip versager;





    public AudioClip ildiko;
    public AudioClip poison;


    private AudioSource source;

    void OnEnable()
    {
        EventManager.StartListening(Events.KEY_DOWN, onTyping);
        EventManager.StartListening(Events.BOTTLE_SUCCESS, onBottleSuccess);
        EventManager.StartListening(Events.BOTTLE_FAILURE, onBottleFailure);
        EventManager.StartListening(Events.POISON, onPoison);
    }

    void OnDisable()
    {
        EventManager.StopListening(Events.KEY_DOWN, onTyping);
        EventManager.StopListening(Events.BOTTLE_SUCCESS, onBottleSuccess);
        EventManager.StopListening(Events.BOTTLE_FAILURE, onBottleFailure);
        EventManager.StopListening(Events.POISON, onPoison);
    }

    void Awake () {
        source = GetComponent<AudioSource>();
    }

    void onTyping(string typingPayload) {
        source.PlayOneShot(keyPress);
    }

    private void onBottleSuccess(string eventPayload) {
        source.PlayOneShot(moveBottle);
        
        if (eventPayload.ToLower().Contains("ildiko")) {
            source.PlayOneShot(ildiko);
        }
    
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

        AudioClip shout = mitP;
        int randomShout = (int) Random.Range(1f, 20f);

        switch(randomShout) {
            case 1:
                source.PlayOneShot(mitP);
                break;
            case 2:
                source.PlayOneShot(mitA);
                break;
            case 3:
                source.PlayOneShot(ohMan);
                break;
            case 4:
                source.PlayOneShot(oops);
                break;
            case 5:
                source.PlayOneShot(passAuf1);
                break;
            case 6:
                source.PlayOneShot(passAuf2);
                break;
            case 7:
                source.PlayOneShot(verdammt);
                break;
            case 8:
                source.PlayOneShot(versager);
                break;
            default:
                break;
        }
        source.PlayOneShot(randomBottleBreak);

    }

    private void onPoison(string eventPayload) {
        Debug.Log("onpoison");
        int random = (int) Random.Range(1f, 10f);
        random = (int) 5f;

        Debug.Log(random == 5);
        if (random == 5) {
            source.PlayOneShot(poison);
        }
    }
}
