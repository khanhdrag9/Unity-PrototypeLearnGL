using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject controller = null;
    public Bullet PRB_bullet = null;
    public float PPT_recoil = 3f;
    public float delayShoot = 0.1f;

    public GameObject point1 = null;
    public GameObject point2 = null;
    public GameObject point3 = null;

    List<GameObject> shootPoint;
    int level = 1;

    float cooldown = 0;
    void Start()
    {
        shootPoint = new List<GameObject>();
        cooldown = 0f;
        UpdateGunTo(level);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(cooldown <= 0)
                Shoot();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateGunTo(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateGunTo(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateGunTo(3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpdateGunTo(4);
        }

        cooldown -= Time.deltaTime;
    }

    public void Shoot()
    {
        float half = shootPoint.Count * 0.5f;
        float startRecoil = 0 - half * PPT_recoil;
        for(int i = 0; i < shootPoint.Count; i++)
        {
            var point = shootPoint[i];
            float angle = controller.transform.localEulerAngles.z;
            float recoil = 0;
            if(level == 1)
            {
                recoil = Random.Range(-PPT_recoil, PPT_recoil);
            }
            else if(level > 2)
            {
                recoil = PPT_recoil * point.transform.localPosition.x * 5;
            }

            Bullet bullet = PoolObjects.Instance.GetFreeObject<Bullet>(PRB_bullet);
            bullet.gameObject.SetActive(true);
            bullet.x = point.transform.position.x;
            bullet.y = point.transform.position.y;
            bullet.Fire(angle + recoil);
        }
        

        cooldown = delayShoot;
    }

    public void UpdateGunTo(int level)
    {
        float startRecoil = 0 - (level * 0.5f) * PPT_recoil;

        this.level = level;
        shootPoint.Clear();
        if(this.level == 1)
        {
            shootPoint.Add(point1);
        }
        else if(this.level == 2)
        {
            shootPoint.Add(point2);
            shootPoint.Add(point3);
        }else if(this.level == 3)
        {
            shootPoint.Add(point2);
            shootPoint.Add(point1);
            shootPoint.Add(point3);
        }
    }
}
