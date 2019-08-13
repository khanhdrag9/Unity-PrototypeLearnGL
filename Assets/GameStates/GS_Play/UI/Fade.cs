using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public float PRP_fadeInDuration = 0.25f;
    public float PRP_fadeOutDuration = 1f;
    public float PRP_targetShow = 0.7f; 
    public bool PRP_onlyShow = false;

    bool isFadeIn = false;
    float cooldown = 0f;
    Image image;
    Color origin;

    void Start()
    {
        image = GetComponent<Image>();
        origin = image.color;
        origin.a = 0;
        image.color = origin;
    }

    void Update()
    {
        float dt = Time.deltaTime;
        if(cooldown > 0 || isFadeIn)
        {
            cooldown -= dt;
            if(isFadeIn)
            {
                Color color = image.color;
                color.a = PRP_targetShow - cooldown /PRP_fadeInDuration;
                if(color.a >= PRP_targetShow || cooldown <= 0)
                {
                    color.a = PRP_targetShow;
                    isFadeIn = false;
                    if(PRP_onlyShow)
                        cooldown = 0;
                    else
                        cooldown = PRP_fadeOutDuration;
                }
                image.color = color;
            }
            else
            {
                Color color = image.color;
                color.a = cooldown /PRP_fadeOutDuration;
                if(color.a <= 0 || cooldown <= 0)
                {
                    color.a = 0;
                    cooldown = 0;
                    GameScreen.Instance.ShowOverSceen();
                }
                image.color = color;
            }
        }
    }

    public void StartEffect()
    {
        isFadeIn = true;
        cooldown = PRP_fadeInDuration;
    }
}
