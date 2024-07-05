using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILayoutControl
{
    public void ExitLayout();
    public void StartLayout(List<UserInfo> users, UserInfo curUser);
}