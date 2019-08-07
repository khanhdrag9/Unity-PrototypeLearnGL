using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ObjectController
{
    [Header("Player")]
    public float bounce = 0.5f;
    public bool isAuto = true;
    public Gun gun = null;
    void Start()
    {
        base.Init();
        base.UpdateTransform(0);
        if(isAuto)
        {
            StartCoroutine(AutoMove());
        }
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
        if(isAuto)
        {
            MoveX(dt);
            MoveY(dt);
        }
        else
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
        }

        if(isAuto)
        {
            foreach(Enemy e in EnemyMgr.Instance.enemies)
            {
                float targetAngle = Helper.AngleFromToBy90D(x ,y , e.x , e.y );
                float subAngle = Helper.AngleBetweenAngle(angle, targetAngle);
                float speedRotate = 350f;
                if(subAngle > 0)
                {
                    angle += speedRotate * dt;
                    // if(angle < targetAngle) angle = targetAngle;
                }
                else if(subAngle < 0)
                {
                    angle -= speedRotate * dt;
                    // if(angle > targetAngle) angle = targetAngle;
                }

                if(gun && gun.CanShoot())
                    gun.Shoot();
                break;
            }
        }
        else
        {
            Vector2 mouse = Input.mousePosition;
            mouse /= GameScreen.pixelUnit;
            mouse.x = (mouse.x * GameScreen.rateScale ) - GameScreen.screenWidth * 0.5f;
            mouse.y = (mouse.y * GameScreen.rateScale ) - GameScreen.screenHeight * 0.5f;
            angle = Helper.AngleFromToBy90D(x ,y , mouse.x , mouse.y );
        }
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

    IEnumerator AutoMove()
    {
        while(true)
        {
            directX = Random.Range(-1, 2);
            directY = Random.Range(-1, 2);
            float delay = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(delay);
        }
    }
}
