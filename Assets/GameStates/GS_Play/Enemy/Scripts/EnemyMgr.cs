using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMgr : Singleton<EnemyMgr>
{
    public float delaySpawn = 3f;
    public Enemy enemy1 = null;
    public Enemy enemy2 = null;
    public Enemy enemy3 = null;
    public GameObject GO_target = null;

    public List<Enemy> enemies;
    public List<EnemyWave> SCO_waves;

    float cooldown = 0;
    void Start()
    {
        enemies = new List<Enemy>();
        cooldown = delaySpawn;
        foreach(var sw in SCO_waves)
            StartCoroutine(SpawnWave(sw));
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        if(delaySpawn >= 0)
        {
            if(cooldown <= 0)
            {
                Spawn();
                cooldown = delaySpawn;
            }
            cooldown -= dt;
        }

    }

    void Spawn()
    {
        Enemy enemy = null;
        // if(Helper.RollRate(35))
        //     enemy = PoolObjects.Instance.GetFreeObject<Enemy>(enemy3);
        // else if(Helper.RollRate(60)) 
        //     enemy = PoolObjects.Instance.GetFreeObject<Enemy>(enemy2);
        // else  
            enemy = PoolObjects.Instance.GetFreeObject<Enemy>(enemy1);
        
        Color color = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
        enemy.GetComponent<SpriteRenderer>().color = color;
        enemy.color = color;
        enemies.Add(enemy);
        enemy.Spawn(GO_target);
    }

    IEnumerator SpawnWave(EnemyWave wave)
    {
        while(true)
        {
            StartCoroutine(wave.Spawn());
            float delay =  wave.PRP_delaySpawnEnemy *  wave.PRP_numberSpawn + wave.PRP_delayTime;
            yield return new WaitForSeconds(delay);
        }
    }

}
