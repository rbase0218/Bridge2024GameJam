using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Framework : MonoBehaviour
{
    protected void Awake()
    {
        SetUp();
    }

    protected abstract void Start();
    
    protected virtual void SetUp(){ }
}