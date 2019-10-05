using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public float spawningDuration = .3f;

    private bool isSpawning = true;
    private bool isDying = false;
    private float dyingDuration = .3f;

    private Vector3 targetScale = new Vector3(10f, 10f, -5f);
    private Vector3 shakeRotationAmount = new Vector3(0, 0, .5f);
    private Vector3 shakePositionAmount = new Vector3(.01f, .01f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3();
        StartCoroutine("spawn");
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator spawn()
    {
        while(isSpawning) {
            iTween.ScaleTo(gameObject, targetScale,spawningDuration);
            if (gameObject.transform.localScale.Equals(targetScale)) {
                isSpawning = false;
            }
            yield return null;
        }
    }

    IEnumerator despawn()
    {
        while(isDying)
        {
            iTween.ShakePosition(gameObject, shakePositionAmount, dyingDuration / 2f);
            iTween.ShakeRotation(gameObject, shakeRotationAmount / 2, dyingDuration);
            yield return null;
        }
    }

    public void DIE()
    {
        isDying = true;
        StartCoroutine("despawn");
        Destroy(gameObject, dyingDuration + 0.2f);
    }


}
