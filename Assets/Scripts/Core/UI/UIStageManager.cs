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

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        Bind<UIStage>(typeof(Stages));
        return true;
    }

    public void Play()
    {
        OpenStage();
    }

    private void OpenStage()
    {
        Get<UI_IntroStage>((int)Stages.IntroStage).OpenScreen();
    }
}
