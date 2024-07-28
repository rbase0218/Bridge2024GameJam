using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Managers : MonoBehaviour
{
    #region # [ Origin ] #
    
    private static Managers _instance = null;
    public static Managers Instance => _instance;
    
    #endregion

    #region # [ Managers ] #
    
    private static UIManager _uiManager = new UIManager();
    private static DataManager _dataManager = new DataManager();
    public static UIManager UI { get { Init(); return _uiManager; } }
    public static DataManager Data { get { Init(); return _dataManager; } }

    #endregion
    
    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
                go = new GameObject { name = "@Managers" };

            _instance = Utils.TryOrAddComponent<Managers>(go);
            DontDestroyOnLoad(go);
            
            _uiManager.Init();
            
            Application.targetFrameRate = 60;
        }
    }
}
