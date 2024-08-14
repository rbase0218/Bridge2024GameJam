using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPersonViewer : UIBase
{
    private enum Texts
    {
        LeftTitle,
        RightTitle,
        LeftName,
        RightName
    }

    private enum Images
    {
        LeftPicture,
        RightPicture
    }
    
    protected override bool Init()
    {
        if(!base.Init())
            return false;

        return true;
    }

    public void BindInstance()
    {
        BindText(typeof(Texts));
        BindImage(typeof(Images));
    }

    public void SetFrame(FrameData leftData, FrameData rightData)
    {
        SetFrameData(leftData, rightData);   
    }

    private void SetFrameData(FrameData leftData, FrameData rightData)
    {
        GetText((int)Texts.LeftTitle).text = leftData.title;
        GetText((int)Texts.RightTitle).text = rightData.title;
        
        GetText((int)Texts.LeftName).text = leftData.name;
        GetText((int)Texts.RightName).text = rightData.name;

        // Left Data만 인질과 요원 이미지가 변경되기 때문에
        // 해당 변수만 체크하면 된다.
        if (leftData.type == 1)
            GetImage((int)Images.LeftPicture).sprite = Managers.Data.Hostage;
        else
            GetImage((int)Images.LeftPicture).sprite = Managers.Data.Black;

        GetImage((int)Images.RightPicture).sprite = Managers.Data.Black;

    }
}
