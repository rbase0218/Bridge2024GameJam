using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowContainer : Dictionary<Type, UIWindow>
{
    public T GetWindow<T>() where T : UIWindow
    {
        return this[typeof(T)] as T;
    }

    public void RemoveWindow(Type type)
    {
        this.Remove(type);
    }

    public void RemoveAllWindow()
    {
        if (Count <= 0) return;
        
        foreach (var window in this)
        {
            var key = window.GetType();
            RemoveWindow(key);
        }
    }
    
    public bool ContainWindow<T>()
    {
        return ContainsKey(typeof(T));
    }
}
