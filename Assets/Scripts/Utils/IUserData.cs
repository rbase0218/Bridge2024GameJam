using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserData
{
    public List<UserInfo> Users { get; set; }
    public UserInfo CurtUser { get; set; }
}