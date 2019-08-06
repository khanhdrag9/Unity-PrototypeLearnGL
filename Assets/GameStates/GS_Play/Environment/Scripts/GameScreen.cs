using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour
{
    public static float fixedScreenHeight = 1080;
    public static float screenWidth;
    public static float screenHeight;
    public static float rateScale;
    public static int pixelUnit = 100;
    void Start()
    {
        rateScale = fixedScreenHeight / Screen.height;
        screenWidth = (Screen.width * rateScale) / pixelUnit;
        screenHeight = fixedScreenHeight / pixelUnit;
        Debug.Log($"SW {screenWidth} - SH {screenHeight} with rate : {rateScale}");
    }
}
