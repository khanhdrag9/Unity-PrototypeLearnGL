using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    public float speed = 0.5f;
    public float size = 1;
    public ParticleSystem PRB_destroyEffect = null;
    public ParticleSystem PRB_collisionEnemy = null;
    public bool fromPlayer = true;
    [Header("General")]
    public float x = 0;
    public float y = 0;
    public float angle = 0;
    public float directX = 0;
    public float directY = 0;

    public void Fire(float angle)
    {
        this.angle = angle;
        directX = Helper.Cos(angle + 90);
        directY = Helper.Sin(angle + 90);
        Update();
    }

    void Update()
    {
        float dt = Time.deltaTime;
        x += directX * speed * dt;
        y += directY * speed * dt;

        //Collision
        float halfW = GameScreen.screenWidth * 0.5f;
        float halfH = GameScreen.screenHeight * 0.5f;
        if(x <= -halfW || x >= halfW)Explose(false, Color.white);
        if(y <= -halfH || y >= halfH)Explose(false, Color.white); 

        //Unity transform
        transform.position = new Vector2(x, y);
        transform.localEulerAngles = new Vector3(0,0,angle);

        if(fromPlayer)
        {
            foreach(var enemy in EnemyMgr.Instance.enemies)
            {
                if(!enemy.gameObject.activeSelf)continue;

                float distance = Helper.Distance(enemy.x, enemy.y, x, y);
                if(distance <= (size + enemy.size) * 0.5f)
                {
                    enemy.Hit(1);
                    Explose(true, enemy.color);
                    break;
                }
            }
        }

    }

    public void Explose(bool isEnemy, Color color)
    {
        gameObject.SetActive(false);
        ParticleSystem explose = null;
        if(isEnemy && PRB_collisionEnemy)
        {
            explose = PoolObjects.Instance.GetFreeObject<ParticleSystem>(PRB_collisionEnemy);

            var main = explose.main;
            main.startColor = color;
            var pares = explose.gameObject.GetComponentsInChildren<ParticleSystem>();
            foreach(var p in pares)
            {
                var pMain = p.main;
                pMain.startColor = color;
            }
        }
        else if(PRB_destroyEffect)
        {
            explose = PoolObjects.Instance.GetFreeObject<ParticleSystem>(PRB_destroyEffect);
            
        }
        if(explose)
        {
            explose.transform.position = new Vector2(x, y);
            explose.gameObject.SetActive(true);
        }
    }
}