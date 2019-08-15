using UnityEngine;

public class EnemyLevel2 : Enemy
{
    [Header("EnemyLevel2")]
    public GameObject target = null;
    public float speedRotate = 100f;
    public override void Update()
    {   
        float dt = Time.deltaTime;
        float targetAngle = Helper.AngleFromToBy90D(x, y, target.transform.position.x, target.transform.position.y);
        float subAngle = Helper.AngleBetweenAngle(angle, targetAngle);
        
        if(subAngle > 0)
        {
            angle += speedRotate * dt;
            if(angle > targetAngle) angle = Helper.NormalizeAngle(targetAngle);
        }
        else if(subAngle < 0)
        {
            angle -= speedRotate * dt;
            if(angle < targetAngle) angle = Helper.NormalizeAngle(targetAngle);
        }
        
        
        base.Update();
    }

    public override void Spawn(GameObject target)
    {
        this.target = target;
        base.Spawn(target);
    }

    
}