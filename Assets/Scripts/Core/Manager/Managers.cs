using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public sealed class Managers : MonoBehaviour
{
    #region # [ Origin ] #
    
    private static Managers _instance = null;
    public static Managers Instance => _instance;
    
    #endregion

    #region # [ Managers ] #
    
    private static DataManager _dataManager;
    private static UIManager _uiManager;
    private static GameManager _gameManager;
    private static SoundManager _soundManager;
    //private static AdsManager _adsManager;
    
    public static UIManager UI { get { Init(); return _uiManager; } }
    public static DataManager Data { get { Init(); return _dataManager; } }
    public static GameManager Game { get { Init(); return _gameManager; } }
    public static SoundManager Sound { get { Init(); return _soundManager; } }
    //public static AdsManager Ads { get { Init(); return _adsManager; } }

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
            _gameManager = Utils.TryOrAddComponent<GameManager>(go);
            _soundManager = Utils.TryOrAddComponent<SoundManager>(go);
            _uiManager = Utils.TryOrAddComponent<UIManager>(go);
            _dataManager = new DataManager();
            //_adsManager = Utils.TryOrAddComponent<AdsManager>(go);
            
            DontDestroyOnLoad(go);
            
            _uiManager.Init();
            _dataManager.Init();
            _soundManager.Init();
            //_adsManager.Init();
            
            Application.targetFrameRate = 60;
        }
    }
    
    private void OnApplicationQuit()
    {
        _dataManager.SaveAudioSettings();
    }
}
