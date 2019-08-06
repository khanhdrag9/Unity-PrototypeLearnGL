using UnityEngine;
using System.Collections.Generic;
public class PoolObjects : Singleton<PoolObjects>
{
    public List<Bullet> bullets;
    public List<ParticleSystem> exploses;

    void Start()
    {
        bullets = new List<Bullet>();
        exploses = new List<ParticleSystem>();
    }

    public Bullet GetBullet()
    {
        for(int i = 0; i < bullets.Count; i++)
        {
            if(!bullets[i].gameObject.activeSelf)
                return bullets[i];
        }
        return null;
    }

    public void AddBullet(Bullet bullet) => bullets.Add(bullet);

    public ParticleSystem GetExploses()
    {
        for(int i = 0; i < exploses.Count; i++)
        {
            if(!exploses[i].gameObject.activeSelf)
                return exploses[i];
        }
        return null;
    }

    public void AddExplose(ParticleSystem e) => exploses.Add(e);
}
