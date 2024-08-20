using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FinalResult : UIScreen
{
    private enum Objects
    {
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
        // 게임에 참여하는 인원 수를 받아온다.
        // 인원수가 1명일 경우 -> Actor or Assassin 승리
        // 인원수가 1명이 아닐 경우 -> Vip 승리
        
        // 승자의 직업을 받아서 Frame 이미지 변경
        
        return true;
    }

    private void OnClickButton()
    {
        
    }

    private void ShowNameField(params string[] names)
    {
        for (int i = 1; i < 3; ++i)
        {
            GetObject(i).SetActive(i < names.Length);
            GetText(i).text = (i < names.Length) ? names[i] : "";
        }
    }
}