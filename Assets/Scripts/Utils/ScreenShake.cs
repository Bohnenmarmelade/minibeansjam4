using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ScreenShake : MonoBehaviour
{
    public int shakeMultiplier = 2;
    public int defaultShakeDuration = 2;

    void OnEnable()
    {
        EventManager.StartListening(Events.SHAKE, shake);
    }


    public void shake(string shakeDuration)
    {
        int duration = defaultShakeDuration;

        int.TryParse(shakeDuration, out duration);
        Debug.Log("duration string:" + duration);
        shake(duration);
    }

    public void shake(int shakeDuration) {
        Debug.Log("duration int:" + shakeDuration);
        iTween.ShakePosition(gameObject, new Vector3(.1f * shakeMultiplier, .1f * shakeMultiplier, .1f * shakeMultiplier), 1 * shakeDuration);
    }

}
