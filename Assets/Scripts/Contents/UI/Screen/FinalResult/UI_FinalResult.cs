using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_FinalResult : UIScreen
{
    private enum Objects
    {
        NameField1,
        NameField2,
        NameField3,
        NameField4
    }

    private enum Buttons
    {
        Button
    }

    private enum Images
    {
        JobImage
    }

    private enum Texts
    {
        Text1,
        Text2,
        Text3,
        Text4
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        GetButton((int)Buttons.Button).onClick.AddListener(OnClickButton);

        return true;
    }

    protected override bool EnterWindow()
    {
        Managers.Sound.PlaySFX("GameOver");
        // 승자의 직업을 받는다
        var winner = Managers.Game.GetWinner();
        
        GetImage((int)Images.JobImage).sprite = Managers.Data.GetFrameSprite(winner);
        
        // 우승한 직업의 유저 목록을 가져온다.
        var userNames = Managers.Game.GetAllPlayers().FindAll(user => user.jobType == winner).Select(x => x.userName).ToArray();
        ShowNameField(userNames);

        return true;
    }

    private void OnClickButton()
    {
        Managers.Sound.PlaySFX("Click");
        OnNextScreen<UI_KeywordReveal>();
    }

    private void ShowNameField(params string[] names)
    {
        for (int i = 0; i < 4; ++i)
        {
            GetObject(i).SetActive(i < names.Length);
            GetText(i).text = (i < names.Length) ? names[i] : "";
        }
    }
}