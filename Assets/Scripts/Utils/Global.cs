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

public static class Global
{
    public static readonly string ActorJobText = "광대";
    public static readonly string AssJobText = "암살자";
    public static readonly string VipJobText = "귀빈";
}



