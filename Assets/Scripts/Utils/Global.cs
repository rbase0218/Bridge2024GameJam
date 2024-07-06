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