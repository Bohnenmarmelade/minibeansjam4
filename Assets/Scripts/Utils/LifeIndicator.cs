﻿using Utils;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class LifeIndicator : MonoBehaviour
{
    public GameObject heartPrefab;
    private GameStatsController gameStatsController;
    public float marginTop = 5f;

    private List<GameObject> currentLifes;

    private Vector3 topLeft;
    private Vector3 positionInWorldSpace;

    private Canvas canvas;


    // Start is called before the first frame update
    void Start()
    {
        gameStatsController = GameObject.FindGameObjectsWithTag("_app")[0].GetComponent(typeof(GameStatsController)) as GameStatsController;
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

        StartCoroutine("initLifeBar");

        EventManager.StartListening(Events.LOSE_LIFE, LoseLife);
        EventManager.StartListening(Events.GAME_OVER, stopListening);
        
        Debug.Log("Top Left Corner is at: " + topLeft.ToString());
        Debug.Log("Life Indicator is at: " + positionInWorldSpace.ToString());
    }

    public void LoseLife(string _)
    {
        GameObject last = currentLifes[currentLifes.Count - 1];

        last.GetComponent<Life>().DIE();
        //Destroy(last);
        currentLifes.Remove(last);
    }

    private void stopListening(string _)
    {
        EventManager.StopListening(Events.BOTTLE_FAILURE, LoseLife);
    }

    IEnumerator initLifeBar()
    {
        Vector3 nextPos = positionInWorldSpace;
        for (int i = 0; i < gameStatsController.currentLifes; i++)
        {
            GameObject heart = Instantiate(heartPrefab, nextPos, Quaternion.identity);
            heart.transform.SetParent(canvas.transform, false);
            currentLifes.Add(heart);
            nextPos.x += heartPrefab.GetComponent<RectTransform>().rect.width * heartPrefab.transform.localScale.x;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
