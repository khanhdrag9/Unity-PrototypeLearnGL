using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public GameObject OBJ_content = null;
    public float PRP_speed = 1f;
    public float PRP_minUnit = 0.1f;
    public Color PRP_lowColor;
    public float current{get; private set;}
    float target;
    Color origin;
    void Start()
    {
        origin = OBJ_content.GetComponent<Image>().color;
        current = OBJ_content.transform.localScale.x;
        target = current;
    }

    void Update()
    {
        float dt = Time.deltaTime;
        float unitSpeed = Mathf.Abs(target - current);
        if(unitSpeed > PRP_minUnit)unitSpeed = PRP_minUnit;
        if(target > current)
        {  
            current = current + PRP_speed * dt * unitSpeed;
            if(target < current)current = target;
        }
        else if(target < current)
        {
            current = current - PRP_speed * dt * unitSpeed;
            if(target > current)current = target;
        }   

        Vector3 scale = OBJ_content.transform.localScale;
        OBJ_content.transform.localScale = new Vector3(current, scale.y, scale.z);
        OBJ_content.GetComponent<Image>().color = origin - (origin - PRP_lowColor) * (1f -current / 1f);
    }

    public void SetProgress(float value)
    {
        value = Mathf.Clamp(value, 0f, 1f);
        target = value;
    }

}
