using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : Singleton<GameScreen>
{
    public static float fixedScreenHeight = 1080;
    public static float screenWidth;
    public static float screenHeight;
    public static float rateScale;
    public static int pixelUnit = 100;

    public Player OBJ_player;
    public float PRP_durationShake = 0.25f;
    public float PRP_amountShake = 0.25f;
    public Fade OBJ_overEffect = null;
    public Fade OBJ_gameover = null;

    float cooldown = 0;
    Vector3 cameraInitPos;
    // public float 
    void Start()
    {
        rateScale = fixedScreenHeight / Screen.height;
        screenWidth = (Screen.width * rateScale) / pixelUnit;
        screenHeight = fixedScreenHeight / pixelUnit;
        Debug.Log($"SW {screenWidth} - SH {screenHeight} with rate : {rateScale}");

        cameraInitPos = Camera.main.transform.position;
        OBJ_gameover.gameObject.SetActive(false);
    }

    void Update()
    {
        float dt = Time.deltaTime;
        if(cooldown > 0)
        {
            float x = Random.Range(-1, 1) * PRP_amountShake * cooldown / PRP_durationShake;
            float y = Random.Range(-1, 1) * PRP_amountShake * cooldown / PRP_durationShake;
            Camera.main.transform.position = new Vector3(cameraInitPos.x + x, cameraInitPos.y + y , cameraInitPos.z);

            cooldown -= dt;    
            if(cooldown <= 0)
            {
                Camera.main.transform.position = cameraInitPos;
            }
        }
    }

    public void Shake()
    {
        Camera.main.transform.position = cameraInitPos;
        cooldown = PRP_durationShake;
    }

    public void GameOver()
    {
        if(OBJ_overEffect)
        {
            OBJ_overEffect.StartEffect();
        }
    }

    public void ShowOverSceen()
    {
        OBJ_gameover.gameObject.SetActive(true);
        OBJ_gameover.StartEffect();
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
