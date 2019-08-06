using UnityEngine;

public class EnemyBasic : Enemy
{
    // [Header("Enemy Basic")]

    public override void Update()
    {   
        if( x > GameScreen.screenWidth * 0.5f + size || 
            x < -GameScreen.screenWidth * 0.5f - size ||
            y > GameScreen.screenHeight * 0.5f + size ||
            y < -GameScreen.screenHeight * 0.5f - size)
        {
            gameObject.SetActive(false);
        }
        
        base.Update();
    }

    public override void Spawn(GameObject target)
    {
        base.Spawn(target);
    }

    
}