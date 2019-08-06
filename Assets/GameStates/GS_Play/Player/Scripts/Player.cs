using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ObjectController
{
    [Header("Player")]
    public float bounce = 0.5f;
    void Start()
    {
        base.Init();
        base.UpdateTransform(0);
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        UpdateCollision(dt);
        UpdateControl(dt);
        base.UpdateTransform(dt);
    }

    void UpdateControl(float dt)
    {
        if(Input.GetKey(KeyCode.A))
        {
            directX = -1;
            MoveX(dt);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            directX = 1;
            MoveX(dt);
        }
        else 
        {
            if(StopX(dt, false))directX = 0;
        }

        if(Input.GetKey(KeyCode.W))
        {
            directY = 1;
            MoveY(dt);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            directY = -1;
            MoveY(dt);
        }
        else
        {
            if(StopY(dt, false))directY = 0;
        }

        Vector2 mouse = Input.mousePosition;
        mouse /= GameScreen.pixelUnit;
        mouse.x = (mouse.x * GameScreen.rateScale ) - GameScreen.screenWidth * 0.5f;
        mouse.y = (mouse.y * GameScreen.rateScale ) - GameScreen.screenHeight * 0.5f;
        angle = Helper.AngleFromToBy90D(x ,y , mouse.x , mouse.y );
    }
    void UpdateCollision(float dt)
    {
        float halfW = GameScreen.screenWidth / 2;
        float halfH = GameScreen.screenHeight / 2;
        if(x <= -halfW)
        {
            directX = bounce;
            x = -halfW;
        }
        else if(x >= halfW)
        {
            directX = -bounce;
            x = halfW;
        }

        if(y <= -halfH)
        {
            directY = bounce;
            y = -halfH;
        }
        else if(y >= halfH)
        {
            directY = -bounce;
            y = halfH;
        }
    }
}
