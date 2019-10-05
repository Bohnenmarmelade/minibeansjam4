using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using System.Collections.Generic;

public class RenderHighscore : MonoBehaviour
{
    public Text highScoreText;
    private int MAX_SCORES = 5;

    private void OnEnable() {
        SetTextFieldsContent();
    }
    private void SetTextFieldsContent()
    {   
        GameStatsController gameStatsController = GameObject.FindGameObjectsWithTag("_app")[0].GetComponent(typeof(GameStatsController)) as GameStatsController;
        List<int> entries = gameStatsController.highScoreList;
        entries.Sort((x,y)=> y.CompareTo(x));
        int size = entries.Count;
        Debug.Log("size is: " + size);
        for (int i = 0; i < MAX_SCORES && i < size; i++) {
            int spot = i+1;
            highScoreText.text += "\n <b>" + spot + " - " + entries[i] + "</b>";
        }

    }
}
