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
}