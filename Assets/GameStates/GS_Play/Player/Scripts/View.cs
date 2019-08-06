using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public GameObject target = null;
    void Update()
    {
        if(target)
        {
            transform.position = target.transform.position;
        }
    }
}
