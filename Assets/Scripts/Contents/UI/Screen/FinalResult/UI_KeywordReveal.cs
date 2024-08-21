using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_KeywordReveal : UIScreen
{
    private enum Buttons
    {
        Button
    }

    private enum Texts
    {
        TopicText
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        GetButton((int)Buttons.Button).onClick.AddListener(OnClickButton);
        return true;
    }
    
    protected override bool EnterWindow()
    {
        GetText((int)Texts.TopicText).SetText(Managers.Game.gameTopic);
        
        return true;
    }
    
    private void OnClickButton()
    {
        SceneManager.LoadScene("Title");
    }
}