using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectContainer : UIBase
{
    public enum SelectButtons
    {
        SelectButtons
    }

    public enum Submit
    {
        SubmitButton
    }
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        Bind<UISelectButtons>(typeof(SelectButtons));
        Bind<UISubmitButton>(typeof(Submit));
        
        return true;
    }
    
}
