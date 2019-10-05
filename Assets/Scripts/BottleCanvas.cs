using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCanvas : MonoBehaviour
{
    public void Awake()
    {
        var canvasComponent = gameObject.GetComponent<Canvas>();
        canvasComponent.renderMode = RenderMode.ScreenSpaceCamera;
        canvasComponent.worldCamera = Camera.main;
        canvasComponent.renderMode = RenderMode.WorldSpace;
        canvasComponent.sortingOrder = 42;
    }
}
