using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;
public class ScoreTextInput : MonoBehaviour
{
    public Text scoreText;

    private void OnEnable() {
        SetTextFieldsContent();
    }
    private void SetTextFieldsContent()
    {   
        //float test = GameObject.FindGameObjectsWithTag("_app")[0].transform.position[0];
        GameStatsController test = GameObject.FindGameObjectsWithTag("_app")[0].GetComponent(typeof(GameStatsController)) as GameStatsController;
        scoreText.text = "<b>" + "You Shipped: " + test.score + " ml </b>";
    }
}
