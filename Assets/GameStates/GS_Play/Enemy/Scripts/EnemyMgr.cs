using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMgr : Singleton<EnemyMgr>
{
    public float delaySpawn = 3f;
    public Enemy enemy1 = null;
    public GameObject GO_target = null;
    public List<Enemy> enemies;

    public float cooldown = 0;
    void Start()
    {
        enemies = new List<Enemy>();
        cooldown = delaySpawn;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        if(cooldown <= 0)
        {
            Spawn();
            cooldown = delaySpawn;
        }
        cooldown -= dt;
    }

    void Spawn()
    {
        var enemy = PoolObjects.Instance.GetFreeObject<Enemy>(enemy1);
        enemies.Add(enemy);
        enemy.Spawn(GO_target);
    }

}
