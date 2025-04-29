using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EJobType
{
    None,
    VIP,
    Assassin,
    Actor
}

public static class Global
{
    public static readonly string ActorJobText = "광대";
    public static readonly string AssJobText = "암살자";
    public static readonly string VipJobText = "귀빈";
}


// Font의 Size를 조정하는 곳에 사용되는 Static Class
public static class FontSize
{
    public static readonly float Font60 = 60f;
    public static readonly float Font40 = 40f;
    public static readonly float Font20 = 20f;
}

public class UserInfo
{
    
    // 유저 이름
    public string userName;
    // 유저 직업
    public EJobType jobType;
    public bool canQuestion;
    public bool isDie;
    
    public UserInfo(string userName)
    {
        this.userName = userName;
        jobType = EJobType.VIP;
        canQuestion = true;
    }
    
    public UserInfo(string userName, EJobType jobType)
    {
        this.userName = userName;
        this.jobType = jobType;
        canQuestion = true;
    }
}

public class FrameData
{
    public string title;
    public string name;
    public int type;

    public FrameData(string title, string name, int type)
    {
        this.title = title;
        this.name = name;
        this.type = type;
    }
}

public class VoteData
{
    public int voteCount;
    public UserInfo targetUser;
    
    public VoteData(int voteCount, UserInfo targetUser)
    {
        this.voteCount = voteCount;
        this.targetUser = targetUser;
    }
}