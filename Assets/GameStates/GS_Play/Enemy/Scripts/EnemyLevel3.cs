using UnityEngine;

public class EnemyLevel3 : EnemyBasic
{
    [Header("EnemyLevel3")]
    public GameObject target = null;
    public Bullet PRB_bullet = null;
    public float delayShoot = 0.5f;
    float cooldown = 0;
    public override void Update()
    {   
        float dt = Time.deltaTime;
        if(cooldown <= 0 && target)
        {
            float targetAngle = Helper.AngleFromToBy90D(x, y, target.transform.position.x, target.transform.position.y);
            Bullet bullet = PoolObjects.Instance.GetFreeObject<Bullet>(PRB_bullet);
            bullet.x = x;
            bullet.y = y;
            bullet.Fire(targetAngle);
            bullet.GetComponent<SpriteRenderer>().color = color;
            cooldown = delayShoot;
        }
        cooldown -= dt;
        
        base.Update();
    }

    public override void Spawn(GameObject target)
    {
        cooldown = delayShoot;
        this.target = target;
        base.Spawn(target);
    }
    
}