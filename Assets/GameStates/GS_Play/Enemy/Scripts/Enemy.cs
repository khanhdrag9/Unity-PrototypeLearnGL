using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy")]
    public ParticleSystem PRB_destroyEffect = null;
    public int PRP_hitpoint = 3;
    public int currentHP {get; protected set;}
    [Header("General")]
    public float x = 0;
    public float y = 0;
    public float angle = 0;
    public float size = 1;
    public float speed = 0;
    public float directX = 0;
    public float directY= 0;
    public Color color = Color.white;

    public virtual void Start()
    {
        color =  GetComponent<SpriteRenderer>().color;
    }

    public virtual void Update()
    {
        float dt = Time.deltaTime;

        directX = Helper.Cos(angle + 90);
        directY = Helper.Sin(angle + 90);

        x += directX * speed * dt;
        y += directY * speed * dt;

        transform.position = new Vector2(x, y);
        transform.localEulerAngles = new Vector3(0,0,angle);
    }

    public virtual void Spawn(GameObject target)
    {
        currentHP = PRP_hitpoint;
        x = Random.Range(-GameScreen.screenWidth * 0.5f, GameScreen.screenWidth * 0.5f);
        y = GameScreen.screenHeight * 0.5f + size;
        if(Helper.RollRate(50))y *= -1;

        Transform targetTrans = target.transform;
        this.angle = Helper.AngleFromToBy90D(x, y, targetTrans.position.x, targetTrans.position.y);

        Update();
    }

    public virtual void Hit(int damage)
    {
        currentHP-=damage;
        if(currentHP <= 0)
        {
            Explose();
        }   
    }

    public virtual void Explose()
    {
        if(PRB_destroyEffect)
        {
            var effect = PoolObjects.Instance.GetFreeObject<ParticleSystem>(PRB_destroyEffect);
            effect.transform.position = new Vector2(x, y);
            
            var main = effect.main;
            main.startColor = color;
            var pares = effect.gameObject.GetComponentsInChildren<ParticleSystem>();
            foreach(var p in pares)
            {
                var pMain = p.main;
                pMain.startColor = color;
            }
        }
        gameObject.SetActive(false);
        EnemyMgr.Instance.enemies.Remove(this);
    }
}