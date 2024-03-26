using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{

    private bool isTargeted;
    
    public bool IsTargeted()
    {
        return isTargeted;

    }

    public void SetTarget()
    {
        isTargeted = true;   
    }
}
