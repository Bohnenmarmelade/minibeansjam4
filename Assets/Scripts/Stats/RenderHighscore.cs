using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;
public class RenderHighscore : MonoBehaviour
{
    public Text highScoreText;

    private void OnEnable() {
        SetTextFieldsContent();
    }
    private void SetTextFieldsContent()
    {   
        //float test = GameObject.FindGameObjectsWithTag("_app")[0].transform.position[0];
        GameStatsController gameStatsController = GameObject.FindGameObjectsWithTag("_app")[0].GetComponent(typeof(GameStatsController)) as GameStatsController;
        //entries gameStatsController.highScoreList.Sort;
        highScoreText.text = "<b>" + "Your Score is: " + "" + "</b>";
    }
}
