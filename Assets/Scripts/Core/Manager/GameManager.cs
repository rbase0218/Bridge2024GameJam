using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private GamePlayers _gamePlayers;
    private TopicPicker _topicPicker;
    private QuestionerPicker _questionerPicker;
    public bool isGameEnd;

    private void Awake()
    {
        Initialized();
    }
    
    private void Start()
    {
        if (Managers.Game.isGameEnd)
        {
            Managers.Sound.SetBGMVolume(Managers.Data.BGMVolume / 0.25f);
        }
        else
        {
            Managers.Sound.SetBGMVolume(Managers.Data.BGMVolume);
        }
        Managers.Sound.StopSFX();
        Managers.Sound.PlayBGM("Title");
    }

    private void Initialized()
    {
        _gamePlayers = new GamePlayers();
        _topicPicker = new TopicPicker();
        _questionerPicker = new QuestionerPicker();
    }

    public bool HandlePlayerActions(List<string> playerNames)
    {
        // Nickname -> PlayerData 생성
        var onGenerate = _gamePlayers.GeneratePlayersData(playerNames);
        if (!onGenerate)
            Debug.Log("게임 플레이어 생성에 실패했습니다.");

        // 유저들에게 직업을 분배한다.
        var onRegisterJobs = _gamePlayers.AllocatePlayerJobs();
        
        return onRegisterJobs;
    }
}