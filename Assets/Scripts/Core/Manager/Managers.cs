using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Managers : MonoBehaviour
{
    private static Managers _instance = null;
    public static Managers Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("@Managers");
                if (go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                _instance = go.GetComponent<Managers>();
            }

            return _instance;
        }
    }

    private static UIManager _uiManager = new UIManager();
    private static DataManager _DataManager = new DataManager();
    public static UIManager UI => _uiManager;
    public static DataManager Data => _DataManager;

    private void Start()
    {
        SetUp();
    }

    private void SetUp()
    {
        DontDestroyOnLoad(this);
    }
}