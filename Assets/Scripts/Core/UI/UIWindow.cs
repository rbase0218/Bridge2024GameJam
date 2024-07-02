using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIWindow : MonoBehaviour
{
    public virtual void Init()
    {
        Open();
    }
    public void Hide() => gameObject.SetActive(false);
    public void Open() => gameObject.SetActive(true);

    protected virtual void Awake()
    {
        Managers.UI.AddWindow(this);
    }
}