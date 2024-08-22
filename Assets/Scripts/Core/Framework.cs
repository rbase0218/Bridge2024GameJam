using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Framework : MonoBehaviour
{
    protected void Awake()
    {
        SetUp();
    }

    protected virtual void Start() { }
    
    protected virtual void SetUp(){ }
}
