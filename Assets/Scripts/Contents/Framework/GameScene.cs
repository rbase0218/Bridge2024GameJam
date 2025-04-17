using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameScene : Framework
{
    private bool isPlay;

    protected override void SetUp()
    {
        isPlay = false;
        Managers.Sound.SetBGMVolume(Managers.Data.BGMVolume * 0.35f);
    }

    private void Update()
    {
// #if !UNITY_EDITOR
        if (isPlay == false)
        {
            isPlay = true;
            Managers.UI.ShowWindow<UI_Intro01>();
        }
// #else
//         if(Input.GetKeyDown(KeyCode.Space))
//         {
//             Managers.UI.ShowWindow<UI_Intro01>();
//         }
// #endif
//     }
    }
}
