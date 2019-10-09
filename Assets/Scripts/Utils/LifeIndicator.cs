using Utils;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class LifeIndicator : MonoBehaviour
{
    public GameObject heartPrefab;
    public Vector3 firstPosition;
    public float marginTop = 5f;
    public Vector3 positionOffset = new Vector3(); //must be a multiple of 1/16 unit
    
    private List<GameObject> currentLifes;

    private GameStatsController gameStatsController;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("_app"))
            gameStatsController = GameObject.FindGameObjectsWithTag("_app")[0].GetComponent<GameStatsController>();
        currentLifes = new List<GameObject>();

        StartCoroutine("initLifeBar");

        EventManager.StartListening(Events.LOSE_LIFE, LoseLife);
        EventManager.StartListening(Events.GAME_OVER, stopListening);
        
        Debug.Log("Top Left Corner is at: " + firstPosition.ToString());
        //Debug.Log("Life Indicator is at: " + positionInWorldSpace.ToString());
    }

    public void LoseLife(string _)
    {
        GameObject last = currentLifes[currentLifes.Count - 1];
        
        last.GetComponent<Life>().DIE();
        currentLifes.Remove(last);
    }

    private void stopListening(string _)
    {
        EventManager.StopListening(Events.BOTTLE_FAILURE, LoseLife);
    }

    IEnumerator initLifeBar()
    {
        Vector3 nextPosition = firstPosition;
        int lifes = 10;
        if (gameStatsController)
            lifes = gameStatsController.currentLifes;
        for (int i = 0; i < lifes; i++)
        {
            GameObject heart = Instantiate(heartPrefab, nextPosition, Quaternion.identity);
            heart.transform.SetParent(transform, false);
            currentLifes.Add(heart);
            nextPosition += positionOffset;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
