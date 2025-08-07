using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Sequence02 : UIScreen
{
    private enum PersonViewer
    {
        PersonViewer
    }

    private enum Buttons
    {
        NextButton
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        Bind<UIPersonViewer>(typeof(PersonViewer));
        Get<UIPersonViewer>((int)PersonViewer.PersonViewer).BindInstance();
        BindButton(typeof(Buttons));
        var nextButton = GetButton((int)Buttons.NextButton);
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(OnClickNextButton);

        return true;
    }

    protected override bool EnterWindow()
    {
        var currentPlayer = Managers.Game.GetCurrentPlayer();
        if (currentPlayer.isDie)
        {
            // 살아있는 첫 번째 플레이어를 찾을 때까지 UpdateNextPlayer() 호출
            while (currentPlayer.isDie && !Managers.Game.IsLastPlayer())
            {
                Managers.Game.UpdateNextPlayer();
                currentPlayer = Managers.Game.GetCurrentPlayer();
            }

            if (Managers.Game.IsLastPlayer() && currentPlayer.isDie)
            {
                Debug.Log("모든 플레이어가 죽었거나 마지막 플레이어인 경우");
                return true;
            }
        }

        string hostageName = Managers.Game.GetCurrentHostage().userName;
        string currUserName = currentPlayer.userName;

        Get<UIPersonViewer>((int)PersonViewer.PersonViewer).SetFrame(
            new FrameData("인질", hostageName, 1),
            new FrameData("다음 순서", currUserName, 0)
        );

        if (UseAutoNextScreen)
            BindNextScreen<UI_QuestionInput>();

        return true;
    }

    private void OnClickNextButton()
    {
        Managers.Sound.PlaySFX("Click");
        OnNextScreen<UI_QuestionInput>();
    }
}
