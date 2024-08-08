using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EJobType
{
    None,
    VIP,
    Assassin,
    Clown
}

public enum ELayoutName
{
    Intro,
    NextOrder,
    JobOpen,
    WordCheck,
    QuestionIntro,
    Question,
    Answer,
    Debate,
    VoteIntro,
    NextOrderVote,
    Vote,
    Result,
    Final,
    End
}

public enum ESelectType
{
    Hostage,
    Question,
    Vote,
}

public enum EVoteType
{
    Same,
    VIP,
    Assassin,
    Clown
}

public enum EPageType
{
    Intro,
    WordCheck,
    Question,
    Answer,
    Vote,
    Result
}

public static class Global
{
    // Jobs
    public static readonly string ActorJobText = "광대";
    public static readonly string AssJobText = "암살자";
    public static readonly string VipJobText = "귀빈";
    
    // NextOrder Texts
    public static readonly string NextOrderFrontText = "다음 순서는";
    public static readonly string NextOrderBackText = "입니다.";
}


// Font의 Size를 조정하는 곳에 사용되는 Static Class
public static class FontSize
{
    public static readonly float Font60 = 60f;
    public static readonly float Font40 = 40f;
    public static readonly float Font20 = 20f;
}



