using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInfo_Players : UIWindow
{
    private enum Texts
    {
        HostageText
    }

    private enum PlayersScrollViews
    {
        ScrollView
    }

    protected override bool Init()
    {
        if (!base.Init())
            return false;

        BindText(typeof(Texts));
        Bind<PlayersScrollView>(typeof(PlayersScrollViews));
        
        // PlayersScrollView 바인딩 및 초기화
        Get<PlayersScrollView>((int)PlayersScrollViews.ScrollView).Binding();

        Open();
        return true;
    }

    protected override bool EnterWindow()
    {
        GetText((int)Texts.HostageText).text = Managers.Data.Hostage.name;
        
        // 플레이어 목록 갱신을 지연시켜 실행
        Invoke(nameof(RefreshPlayerList), 0.1f);
        
        return true;
    }

    private void RefreshPlayerList()
    {
        Get<PlayersScrollView>((int)PlayersScrollViews.ScrollView).RefreshPlayerList();
    }
}