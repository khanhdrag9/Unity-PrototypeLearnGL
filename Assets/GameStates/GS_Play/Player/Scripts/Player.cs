using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Player : ObjectController
{
    [Header("Player")]
    public int PRP_hitPoint = 10;
    public float PRP_bounce = 0.5f;
    public float PRP_size = 1.5f;
    public ParticleSystem PRB_destroyEffect = null;
    public int PRP_numberExplodeDestroy = 5;
    public float PRP_distanceExplode = 0.5f;
    public int PRP_numberExploseFinal = 5;
    public float PRP_timeEachExplode = 0.2f;
    public ProgressBar OBJ_hpBar = null;
    public bool isAuto = true;
    public Gun gun = null;

    float cooldown = 0;
    float countExplode = 0;

    public int currentHP{get; private set;}
    void Start()
    {
        base.Init();
        base.UpdateTransform(0);
        currentHP = PRP_hitPoint;
        if(isAuto)
        {
            StartCoroutine(AutoMove());
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        if(currentHP > 0)
        {
            UpdateCollision(dt);
            UpdateControl(dt);
            base.UpdateTransform(dt);
        }
        else
            UpdateExplode(dt);
        
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
        //Edge
        float halfW = GameScreen.screenWidth / 2;
        float halfH = GameScreen.screenHeight / 2;
        if(x <= -halfW)
        {
            directX = PRP_bounce;
            x = -halfW;
        }
        else if(x >= halfW)
        {
            directX = -PRP_bounce;
            x = halfW;
        }

        if(y <= -halfH)
        {
            directY = PRP_bounce;
            y = -halfH;
        }
        else if(y >= halfH)
        {
            directY = -PRP_bounce;
            y = halfH;
        }

        //Enemies
        foreach(Enemy enemy in EnemyMgr.Instance.enemies)
        {
            float length = Helper.Distance(x, y, enemy.x, enemy.y);
            if(length <= (PRP_size + enemy.size) * 0.5f)
            {
                Hit(enemy.currentHP);
                enemy.Hit(enemy.currentHP);
                break;
            }
        }
    }
    void UpdateExplode(float dt)
    {
        if(currentHP <= 0)
        {
            if(cooldown <= 0 && countExplode < PRP_numberExplodeDestroy)
            {
                countExplode++;
                cooldown = PRP_timeEachExplode;

                if(countExplode == PRP_numberExplodeDestroy)
                {
                    Explode(PRP_numberExploseFinal);
                    gameObject.SetActive(false);
                    GameScreen.Instance.GameOver();
                }
                else
                {
                    Explode(1);
                }
            }

            cooldown -= dt;
        }
    }
    void Hit(int damage)
    {
        currentHP -= damage;
        GameScreen.Instance.Shake();
        OBJ_hpBar.SetProgress(currentHP / (float)PRP_hitPoint);
        // if(currentHP <= 0)
        // {
        //     // Explode();
        // }
    }

    void Explode(int number)
    {
        if(PRB_destroyEffect)
        {
            for(int i = 0; i < number; i++)
            {
                var effect = PoolObjects.Instance.GetFreeObject<ParticleSystem>(PRB_destroyEffect);
                float px = x + Random.Range(-PRP_distanceExplode, PRP_distanceExplode);
                float py = y + Random.Range(-PRP_distanceExplode, PRP_distanceExplode);
                effect.transform.position = new Vector2(px, py);
            }
        }
    }

    IEnumerator AutoMove()
    {
        while(true)
        {
            directX = Random.Range(-1f, 1f);
            directY = Random.Range(-1f, 1f);
            float delay = Random.Range(0.5f, 1.5f);
            yield return new WaitForSeconds(delay);
        }
    }

    public void AutoPlay(Button button)
    {
        if(!isAuto)
        {
            StartCoroutine(AutoMove());
            isAuto = true;
            button.GetComponent<Image>().color = new Color(0.2784314f, 0.7254902f, 0);
        }
        else
        {
            StopAllCoroutines();
            isAuto = false;
            button.GetComponent<Image>().color = new Color(0.3803922f, 0.3803922f, 0.3803922f);
        }
        
    }
}
