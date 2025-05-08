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

public class UserInfo
{
    // 유저 이름
    public string userName;
    // 유저 직업
    public EJobType jobType;
    
    // 사망자
    public bool isDie;
    // 현재 인질 여부
    public bool isHostage;
    
    public UserInfo(string userName)
    {
        this.userName = userName;
        jobType = EJobType.VIP;
        isDie = false;
        isHostage = false;
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