using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Blink : MonoBehaviour
{
    public RectTransform upperBox;
    public RectTransform lowerBox;

    Vector3 startPosUpperBox;
    Vector3 startPosLowerBox;
    private float rand1;
    private float rand2;
    public float heightProcentage = 0.35f;
    public const float period = 2f;

    protected void Start()
    {
        upperBox.sizeDelta = new Vector2(2000, gameObject.GetComponent<RectTransform>().rect.height * heightProcentage / 2);
        lowerBox.sizeDelta = new Vector2(2000, gameObject.GetComponent<RectTransform>().rect.height * heightProcentage / 2);


        startPosUpperBox = upperBox.position;
        startPosLowerBox = lowerBox.position;
        rand1 = Random.Range(1, 10);
        rand2 = Random.Range(1, 10);
        Destroy(gameObject, 3f);
    }

    protected void Update()
    {
        float theta = Time.time / period;
        float distance = (-Mathf.Cos(rand1 * theta) + 1) / 2 * (-Mathf.Sin(rand2 * theta) + 1) / 2;
        upperBox.position = startPosUpperBox + Vector3.up * distance * 150;
        lowerBox.position = startPosLowerBox + Vector3.down * distance * 150;
    }




}
