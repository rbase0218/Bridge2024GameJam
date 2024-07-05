using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserInfo
{
    public int index;
    public int voteCount;
    public string name;
    public EJobType jobType;
    public bool isSelect;
    public bool curHostage;
    public bool isVotePoint;
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
        this.isSelect = isSelect;
    }
    
    public void SetHostage(bool curHostage)
    {
        this.curHostage = curHostage;
    }
    
    public void SetIcon(Sprite icon)
    {
        this.icon = icon;
    }
    
    public void SetVotePoint(bool isVotePoint)
    {
        this.isVotePoint = isVotePoint;
    }
    
    public void SetVoteCount(int voteCount)
    {
        this.voteCount = voteCount;
    }
    
    public void AddVoteCount()
    {
        voteCount++;
    }
}