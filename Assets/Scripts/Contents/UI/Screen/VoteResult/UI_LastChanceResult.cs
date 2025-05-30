using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class UI_LastChanceResult : UIScreen
{
    private enum Texts
    {
        FirstText,
        JobText,
        SecondText,
    }

    private enum Images
    {
        Frame,
        BG
    }

    private enum Buttons
    {
        NextButton
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        return true;
    }

    private void OnClickNextButton()
    {
        Managers.Sound.PlaySFX("Click");

        OnNextScreen<UI_Switcher01F>();
    }

    public void SetInfo(bool isAnswerCorrect)
    {
        if (isAnswerCorrect)    // 정답을 맞춘 경우
        {
            Managers.Sound.PlaySFX("Correct");
            // Assassin의 승리
            // 다양한 게임 분기를 위해 정답입니다 문구를 제거함.
            GetText((int)Texts.FirstText).SetText("");
            GetText((int)Texts.SecondText).SetText("암살자가\n귀빈들과의 대결에서\n승리했습니다.");
            
            Managers.Game.SetWinner(EJobType.Assassin);
        }
        else
        {
            var voteUser = Managers.Game.GetMaxVotePlayerName()[0];
            var voteUserJob = Managers.Game.FindPlayer(voteUser).jobType;
            
            // 광대 승리
            if (voteUserJob == EJobType.Actor)
            {
                Managers.Sound.PlaySFX("Correct");
        
                GetText((int)Texts.FirstText).SetText("오답입니다!");
                GetText((int)Texts.SecondText).SetText("뜻밖의 광대가\n귀빈들과의 게임에서\n승리를 가져갑니다.");
                
                Managers.Game.SetWinner(EJobType.Actor);
                
            } 
            else if (voteUserJob == EJobType.Assassin) 
            {
                // 귀빈 승리
                Managers.Sound.PlaySFX("Wrong");
        
                GetText((int)Texts.FirstText).SetText("오답입니다!");
                GetText((int)Texts.JobText).SetText("귀빈");
                GetText((int)Texts.SecondText).SetText("귀빈들이\n그들의 무도회를\n지켜냈습니다.");
                
                Managers.Game.SetWinner(EJobType.VIP);
            }
        }
        var winJob = Managers.Game.GetWinner();
        
        var winnerJobFrame = Managers.Data.GetFrameBGSprite(winJob);
        var jobText = Managers.Data.GetJobText(winJob);
        
        GetText((int)Texts.JobText).SetText(jobText);
        GetImage((int)Images.BG).sprite = winnerJobFrame;
        GetImage((int)Images.Frame).sprite = Managers.Data.GetFrameSprite(winJob);
        
        gameObject.SetActive(true);
    }
}