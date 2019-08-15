using System;
using UnityEngine;

public class ExpController : MonoBehaviour
{
    public ProgressBar OBJ_bar;
    public int PRP_totalExp;
    public int currentExp{get; private set;}

    public void Start()
    {
        currentExp = 0;
        OBJ_bar.SetProgress(currentExp / PRP_totalExp);
    }
    public void Reset()
    {
        currentExp = 0;
    }
    public void Increase(int value)
    {
        currentExp = Mathf.Clamp(currentExp + value, 0, PRP_totalExp);
        OBJ_bar.SetProgress(currentExp / PRP_totalExp);
        if(currentExp == PRP_totalExp)
        {
            LevelUp();
            Reset();
            PRP_totalExp += 2;
        }
    }

    private void LevelUp()
    {
        Debug.Log("Level up");
    }
}