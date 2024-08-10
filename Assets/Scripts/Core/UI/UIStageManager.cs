using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStageManager : UIBase
{
    public enum Stages
    {
        IntroStage,
        JobCheckStage,
        QuestionStage,
        VotingStage,
        VoteResultStage,
        FinalResultStage
    }

    [field: SerializeField, Space(15), Header("필요한 인스턴스 요소")]
    public UI_Gauge Gauge;

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        Bind<UIStage>(typeof(Stages));
        if (Gauge == null)
        {
            Debug.LogError("Gauge 인스턴스가 존재하지 않습니다.");
            Debug.Break();
        }
        
        return true;
    }

    public void Play()
    {
        OpenStage();
    }

    private void OpenStage()
    {
        Get<UI_IntroStage>((int)Stages.IntroStage).OpenScreen(Gauge);
    }
}
