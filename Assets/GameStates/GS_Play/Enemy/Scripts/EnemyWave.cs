using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave")]
public class EnemyWave : ScriptableObject
{
    public EnemyMoveWave PFB_enemy = null;
    public GameObject PFB_waveNode = null;
    public float PRP_delayTime = 3f;
    public float PRP_delaySpawnEnemy = 1.5f;
    public float PRP_speedMovement = 2f; 
    public int PRP_numberSpawn = 5;
    
    List<Transform> paths = null;

    public IEnumerator Spawn()
    {
        paths = new List<Transform>();
        foreach(Transform child in PFB_waveNode.gameObject.transform)
        {
            paths.Add(child);
        }
            
        for(int i = 0; i < PRP_numberSpawn; i++)
        {
            var enemy = PoolObjects.Instance.GetFreeObject<Enemy>(PFB_enemy) as EnemyMoveWave;

            Color color = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
            enemy.GetComponent<SpriteRenderer>().color = color;
            enemy.color = color;

            EnemyMgr.Instance.enemies.Add(enemy);
            enemy.speed = PRP_speedMovement;
            enemy.paths = paths;
            enemy.Spawn(null);

            yield return new WaitForSeconds(PRP_delaySpawnEnemy);
        }
    }
}
