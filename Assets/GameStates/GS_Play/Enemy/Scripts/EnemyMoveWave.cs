using UnityEngine;
using System.Collections.Generic;

public class EnemyMoveWave : EnemyLevel2
{
    [Header("EnemyMoveWave")]
    // [HideInInspector]
    public List<Transform> paths = null;
    int pathIndex = 0;

    public override void Spawn(GameObject target)
    {
        currentHP = PRP_hitpoint;
        pathIndex = 0;
        x = paths[pathIndex].position.x;
        y = paths[pathIndex].position.y;
        Debug.Log(x + "/" + y);
        NextPath();
        transform.position = new Vector2(x, y);
    }

    void NextPath()
    {
        pathIndex++;
        target = paths[pathIndex].gameObject;
        // this.angle = Helper.AngleFromToBy90D(x, y, targetPosition.x, targetPosition.y);
    }

    public override void Update()
    {
        float dt = Time.deltaTime;
        Vector2 targetPosition = target.transform.position;
        Vector2 vec = Vector2.MoveTowards(new Vector2(x, y), targetPosition, speed * dt);
        if(vec == targetPosition)
        {
            if(pathIndex < paths.Count - 1)
                NextPath();
            else 
                gameObject.SetActive(false);
        }
        base.Update();
    }
}