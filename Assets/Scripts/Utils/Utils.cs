using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;

public class Utils
{
    public static T TryOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        var isEmpty = go.TryGetComponent<T>(out var component);
        if (!isEmpty)
            component = go.AddComponent<T>();

        return component;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            Transform transform = go.transform.Find(name);
            if (transform != null)
                return transform.GetComponent<T>();
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform != null)
            return transform.gameObject;
        return null;
    }

    public static List<T> MakeChildren<T>(Transform trans, GameObject obj, int count, string name = null) where T : Component
    {
        if (count == 0) 
            return null;

        List<T> resultList = new List<T>();
        
        for (int i = 0; i < count; ++i)
        {
            var gameObj = GameObject.Instantiate(obj, trans);
            resultList.Add(FindChild<T>(gameObj, gameObj.name));
        }

        return resultList;
    }
}
