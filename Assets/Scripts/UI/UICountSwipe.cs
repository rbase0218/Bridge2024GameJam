using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UICountSwipe : UISwipe
{
    protected override void RegisterData()
    {
        data = Managers.Data.personArray.ToList();
    }
}
