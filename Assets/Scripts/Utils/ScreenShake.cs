using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    public int shakeMultiplier = 2;
    public int shakeLength = 2;
    // Start is called before the first frame update
    void Start()
    {
        //iTween.ShakePosition(gameObject, new Vector3(.1f * shakeMultiplier, .1f * shakeMultiplier, .1f * shakeMultiplier), 1 * shakeLength);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
