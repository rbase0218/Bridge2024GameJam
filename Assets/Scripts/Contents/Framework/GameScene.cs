using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameScene : Framework
{
    private bool isPlay;

    protected override void SetUp()
    {
        isPlay = false;
        Managers.Game.isGameEnd = false;
        Managers.Sound.SetBGMVolume(Managers.Data.BGMVolume * 0.25f);
    }

    private void Update()
    {
        if (isPlay == false)
        {
            isPlay = true;
            Managers.UI.ShowWindow<UI_Intro01>();
        }
    }
}
