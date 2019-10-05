﻿using Utils;
using System.Collections.Generic;
using UnityEngine;

public class LifeIndicator : MonoBehaviour
{
    public GameObject heartPrefab;
    public int maxLifes = 10;
    public float marginTop = 5f;

    private List<GameObject> currentLifes;

    private Vector3 topLeft;
    private Vector3 positionInWorldSpace;

    private Canvas canvas;


    // Start is called before the first frame update
    void Start()
    {
        currentLifes = new List<GameObject>();

        //calculate position of the life indicators (top left for now)
        canvas = gameObject.transform.parent.gameObject.GetComponent<Canvas>();

        RectTransform lifePrefabRectTransform = (RectTransform)heartPrefab.transform;
        Vector3 heartPrefabSize = lifePrefabRectTransform.rect.size * lifePrefabRectTransform.localScale;
        heartPrefabSize.y *= -1;

        RectTransform rectTransform = canvas.GetComponent<RectTransform>();
        float x = rectTransform.rect.xMin;
        float y = (rectTransform.rect.yMin) * -1;
        float z = rectTransform.position.z;

        topLeft = new Vector3(x, y, z);
        positionInWorldSpace = topLeft + heartPrefabSize/2;
        positionInWorldSpace.y -= marginTop;

        initLifeBar();

        EventManager.StartListening(Events.TYPO, loseLife);

        Debug.Log("Top Left Corner is at: " + topLeft.ToString());
        Debug.Log("Life Indicator is at: " + positionInWorldSpace.ToString());
    }

    /*
     *  Returns true if no lifes left
     */
    public void loseLife(string _)
    {
        Debug.Log("life count: " + currentLifes.Count);
        if (currentLifes.Count <= 0)
            return;
        int indexOfLastLife = currentLifes.Count - 1;
        Destroy(currentLifes[indexOfLastLife]);
        currentLifes.RemoveAt(indexOfLastLife);
        if (indexOfLastLife <= 0)
            isDead();
    }

    public void isDead()
    {
        Debug.Log("YOU FAILED");
        EventManager.StopListening(Events.TYPO, loseLife);
        EventManager.TriggerEvent(Events.GAME_OVER);
    }

    public void initLifeBar()
    {
        Vector3 nextPos = positionInWorldSpace;
        for(int i = 0; i < maxLifes; i++)
        {
            GameObject heart = Instantiate(heartPrefab, nextPos, Quaternion.identity);
            heart.transform.SetParent(canvas.transform, false);
            currentLifes.Add(heart);
            nextPos.x += heart.GetComponent<RectTransform>().rect.width * heart.transform.localScale.x;
        }
    }
}
