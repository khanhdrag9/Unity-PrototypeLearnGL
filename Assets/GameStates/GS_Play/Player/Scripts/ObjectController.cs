using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [Header("Control properties")]
    public float x = 0;
    public float y = 0;
    public float angle = 0;
    public float scaleX = 1;
    public float scaleY = 1;
    public float speedX = 0;
    public float speedY = 0;
    public float directX = 0;
    public float directY = 0;

    [Header("Optional properties")]
    public float maxSpeed = 0;
    public float acceleration = 10f;
    public float drag = 5f;

    protected bool brake = false;
    void Start()
    {
        Init();
        UpdateTransform(0);
    }

    void Update()
    {
        UpdateTransform(Time.deltaTime);
    }

    public void Init()
    {
        x = transform.position.x;
        y = transform.position.y;
        angle = transform.localEulerAngles.z;
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }

    public void UpdateTransform(float dt)
    {
        x += speedX * dt * directX;
        y += speedY * dt * directY;
        transform.position = new Vector2(x, y);
        transform.localEulerAngles = Vector3.forward * angle;
        transform.localScale = new Vector3(scaleX, scaleY, 1);
    }

    public void SetDirect(float forX, float forY)
    {
        directX = forX;
        directY = forY;
    }
    public void Move(float dt)
    {
        MoveX(dt);
        MoveY(dt);
    }
    public bool Stop(float dt, bool immediate)
    {
        bool sx = StopX(dt, immediate);
        bool sy = StopY(dt, immediate);
        return sx && sy;
    }
    public float MoveX(float dt)
    {
        if(Mathf.Abs(speedX) != maxSpeed)
        {
            speedX += acceleration * dt;
            if(acceleration < 0 && speedX < -maxSpeed && speedX != -maxSpeed)
            {
                speedX = -maxSpeed;
            }
            else if(acceleration > 0 && speedX > maxSpeed && speedX != maxSpeed)
            {
                speedX = maxSpeed;
            }
        }
        return speedX;
    }
    public float MoveY(float dt)
    {
        if(Mathf.Abs(speedY) != maxSpeed)
        {
            speedY += acceleration * dt;
            if(acceleration < 0 && speedY < -maxSpeed && speedY != -maxSpeed)
            {
                speedY = -maxSpeed;
            }
            else if(acceleration > 0 && speedY > maxSpeed && speedY != maxSpeed)
            {
                speedY = maxSpeed;
            }
        }
        return speedY;
    }
    public bool StopX(float dt, bool immediate)
    {
        if(immediate)speedX = 0;
        brake = true;
        if(speedX > 0)
        {
            speedX -= drag * dt;
            if(speedX <= 0) speedX = 0;
        }
        else if(speedX < 0)
        {
            speedX += drag * dt;
            if(speedX >= 0)speedX = 0;
        }

        bool result = speedX == 0;
        brake = !result;
        return result;
    }
    public bool StopY(float dt, bool immediate)
    {
        if(immediate)speedY = 0;
        brake = true;
        if(speedY > 0)
        {
            speedY -= drag * dt;
            if(speedY <= 0) speedY = 0;
        }
        else if(speedY < 0)
        {
            speedY += drag * dt;
            if(speedY >= 0)speedY = 0;
        }

        bool result = speedY == 0;
        brake = !result;
        return result;
    }
}
