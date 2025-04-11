using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_JobReveal : UIScreen
{
    private enum Texts
    {
        FirstText,
        JobText,
        ThirdText,
        ButtonText
    }

    private enum Buttons
    {
        NextButton
    }

    private enum Images
    {
        Picture,
        BG
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        BindButton(typeof(Buttons));
        
        return true;
    }
    
    protected override bool EnterWindow()
    {
        var voteUser = Managers.Game.voteUser;
        var voteUserName = voteUser.userName;
        var voteUserPicture = Managers.Data.GetFrameSprite(voteUser.jobType);
        var voteUserJobFrame = Managers.Data.GetFrameBGSprite(voteUser.jobType);
        
        GetText((int)Texts.FirstText).SetText($"{voteUserName}은(는)");

        var infoTexts = Managers.Data.jobInfoTexts[voteUser.jobType];
        // Info Text 추가
        GetText((int)Texts.ThirdText).SetText(infoTexts.Item2);
        // Button Text 변경
        GetText((int)Texts.ButtonText).SetText(infoTexts.Item1);

        // 초상화 세팅
        GetImage((int)Images.Picture).sprite = voteUserPicture;
        GetImage((int)Images.BG).sprite = voteUserJobFrame;
        // 직업명 추가
        GetText((int)Texts.JobText).SetText(Managers.Data.GetJobText(voteUser.jobType));
        
        // Event Bind
        GetButton((int)Buttons.NextButton).onClick.AddListener(OnClickNextButton);
        
        return true;
    }

    private void OnClickNextButton()
    {
        var voteUser = Managers.Game.voteUser;
        var voteUserJob = voteUser.jobType;

        switch (voteUserJob)
        {
            case EJobType.VIP:
                voteUser.isDie = true;

                int hostageCount = Managers.Game._hostageList.Count - 1; // 인질로 잡힌 사람 수 (죽은 사람 포함)
                int aliveNonHostageCount = Managers.Game._userList.Count(x => !x.isDie && !Managers.Game._hostageList.Contains(x)); // 생존 중, 아직 인질 아닌 사람

                Debug.Log($"hostageCount: {hostageCount}, aliveNonHostageCount: {aliveNonHostageCount}, totalUsers: {Managers.Game._userList.Count}");

    // 암살자 승리 조건: 인질 + 인질 아닌 생존자 = 총 인원 수 이상 (즉, 게임이 끝날 수 있는 조건)
                bool isAssassinWin = (hostageCount + aliveNonHostageCount) >= Managers.Game._userList.Count;
                if (isAssassinWin)
                {
                    // 암살자 승리
                    Managers.Game.voteUser = Managers.Game._userList.Find(x => x.jobType == EJobType.Assassin);
                    var lastChanceResult = OnNextScreen<UI_LastChanceResult>();
                    lastChanceResult.SetInfo(true); // true면 암살자 승리
                }
                else
                {
                    // 게임 계속 진행
                    Managers.Game.IsReverse = !Managers.Game.IsReverse;
                    OnNextScreen<UI_Sequence02>();
                }
                break;
            case EJobType.Actor:
            case EJobType.Assassin:
                OnNextScreen<UI_LastChance>();
                break;
        }
    }
}