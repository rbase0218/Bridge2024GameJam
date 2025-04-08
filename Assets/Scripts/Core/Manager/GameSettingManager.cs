using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameManager
{
    // User에게 직업을 부여하는 메서드
    private void GiveUsersJob()
    {
        int userCount = _userList.Count;

        if (userCount < 4)
            return;

        // 암살자 인덱스 먼저 선정
        int assIndex = Random.Range(0, userCount);

        // 암살자와 겹치지 않도록 배우 인덱스 재선정
        int actorIndex = -1;
        if (userCount >= 5)
        {
            do
            {
                actorIndex = Random.Range(0, userCount);
            } while (actorIndex == assIndex);
        }

        // 역할 부여
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