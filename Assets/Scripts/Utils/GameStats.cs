﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{

    public static int MAX_LIFES = 10;
    public int currentLifes = 10;


    void OnEnable()
    {
        EventManager.StartListening("typo", looseLife);
    }

    void OnDisable()
    {
        EventManager.StopListening("typo", looseLife);
    }

    private void looseLife(char c) {
        if(currentLifes > 1){
            currentLifes--;
        }else{
            EventManager.TriggerEvent("gameOver", 'l');
        }

        Debug.Log("looseLife");

    }

    public void addLife() {
        if (currentLifes < MAX_LIFES) {
            currentLifes++;
        }
    }

    public int getCurrentLifes()
    {
        return currentLifes;
    }
}
