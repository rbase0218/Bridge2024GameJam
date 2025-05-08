using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        // string originalText = Managers.Game.gameTopic;
        // string wrappedText = string.Join("\n",
        //     Enumerable.Range(0, (originalText.Length + 6) / 7)
        //         .Select(i => originalText.Substring(i * 7,
        //             Math.Min(6, originalText.Length - i * 7))));
        // GetText((int)Texts.TopicText).SetText(wrappedText);

        return true;
    }

    private void OnClickButton()
    {
        // Managers.Sound.PlaySFX("Click");
        //
        // Managers.Game.ResetData();
        // SceneManager.LoadScene("Title");
        // if (Managers.Ads.interAd.CanShowAd())
        // {
        //     Managers.Ads.ShowAd();
        // }
        // else
        // {
        //     // 광고가 없을 경우
        // }
    }
}