using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class UserInfo
{
    public int index;
    public string name;
    public EJobType jobType;
    public bool hasHostage;
    public bool curHostage;
    public bool hasQuestion;
    public bool myTurn;
    public bool isDead;
    public Sprite icon;

    public UserInfo(int index = 0, string name = "", EJobType jobType = EJobType.None)
    {
        SetData(index, name, jobType);
    }

    public void SetData(int index, string name, EJobType jobType, bool isSelect = false)
    {
        this.index = index;
        this.name = name;
        this.jobType = jobType;
        this.hasHostage = isSelect;
    }
}