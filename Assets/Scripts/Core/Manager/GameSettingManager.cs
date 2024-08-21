using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameManager
{
    // User에게 직업을 부여하는 메서드
    private void GiveUsersJob()
    {
        var copyUserList = new List<UserInfo>(_userList);

        int actorIndex = -1;
        int assIndex = Random.Range(0, copyUserList.Count);
        copyUserList.RemoveAt(assIndex);
        
        if (_userList.Count >= 5)
            actorIndex = Random.Range(0, copyUserList.Count);

        // Assassin 직업 부여
        _userList[assIndex].jobType = EJobType.Assassin;
        assUser = _userList[assIndex];
        
        if (actorIndex != -1)
        {
            _userList[actorIndex].jobType = EJobType.Actor;
            actorUser = _userList[actorIndex];
        }
    }
    
    private UserInfo RandomQuestionUser()
    {
        var copyUserList = new List<UserInfo>(_userList);
        copyUserList.Remove(hostageUser);
        copyUserList.RemoveAll(x=>x.canQuestion == false);
        copyUserList.RemoveAll(x=>x.isDie);

        if (copyUserList.Count == 0)
            return null;
        return copyUserList[Random.Range(0, copyUserList.Count)];
    }
}