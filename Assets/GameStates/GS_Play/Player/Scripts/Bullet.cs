using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet")]
    public float speed = 0.5f;
    public float size = 1;
    public ParticleSystem PRB_destroyEffect = null;
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
        if(x <= -halfW || x >= halfW)Explose();
        if(y <= -halfH || y >= halfH)Explose(); 

        //Unity transform
        transform.position = new Vector2(x, y);
        transform.localEulerAngles = new Vector3(0,0,angle);

        foreach(var enemy in EnemyMgr.Instance.enemies)
        {
            if(!enemy.gameObject.activeSelf)continue;

            float distance = Helper.Distance(enemy.x, enemy.y, x, y);
            if(distance <= (size + enemy.size) * 0.5f)
            {
                enemy.Hit(1);
                Explose();
                break;
            }
        }
    }

    public void Explose()
    {
        gameObject.SetActive(false);
        if(PRB_destroyEffect)
        {
            var explose = PoolObjects.Instance.GetFreeObject<ParticleSystem>(PRB_destroyEffect);
            explose.transform.position = new Vector2(x, y);
            explose.gameObject.SetActive(true);
        }
    }
}