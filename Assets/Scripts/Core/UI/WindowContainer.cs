using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowContainer : Dictionary<Type, UIWindow>
{
    // WindowContainer
    // 게임 내에서 존재하는 UIWindow의 상태에 구분 없이 인스턴스를 관리하는 Container
    // Get / Set / Remove / RemoveAllWindow 메서드를 제공한다.
    
    public T GetWindow<T>() where T : UIWindow
    {
        return this[typeof(T)] as T;
    }
    
    public bool ContainWindow<T>()
    {
        return ContainsKey(typeof(T));
    }

    public void RemoveWindow(Type type)
    {
        Remove(type);
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
}
