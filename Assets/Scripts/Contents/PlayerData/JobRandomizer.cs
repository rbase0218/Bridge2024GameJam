using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class JobRandomizer
{
    public static bool SelectRandomJob(List<UserInfo> players, UserInfo assassin, UserInfo joker = null)
    {
        if (players == null || players.Count < 4) 
        {
            Debug.LogError("Error : 플레이어 인원 부족!");
            return false;
        }
        
        // 인원 수에 맞는 직업군을 가져온다.
        var jobs = GetNeedJobs(players.Count);
        if (jobs == null)
        {
            Debug.LogError("Error : 직업 목록을 생성할 수 없습니다!");
            return false;
        }
        
        // 순서대로 가져온 직업을 섞는다.
        Shuffle(jobs);
        
        // 플레이어에게 직업을 부여한다.
        for (int i = 0; i < players.Count; i++)
        {
            players[i].jobType = jobs[i];
            
            if(jobs[i] == EJobType.Assassin)
                assassin = players[i];
            else if(jobs[i] == EJobType.Actor && joker != null)
                joker = players[i];
        }

        return true;
    }

    private static List<EJobType> GetNeedJobs(int count)
    {
        var jobList = new List<EJobType>();
        
        int vipCount = 0;
        bool needActor = false;
        
        // 게임에 필요한 직업을 계산한다.
        // 인원수(count)에 따라서 직업을 설정한다.
        switch (count)
        {
            case 4:
                vipCount = 3;
                break;
            case 5:
                vipCount = 3;
                needActor = true;
                break;
            case 6:
                vipCount = 4;
                needActor = true;
                break;
            default:
                Debug.LogError("Error : 플레이어 인원 부족!");
                return null;
        }
        
        jobList.Add(EJobType.Assassin);
        
        for (int i = 0; i < vipCount; i++)
            jobList.Add(EJobType.VIP);
        
        if (needActor)
            jobList.Add(EJobType.Actor);
        
        return jobList;
    }

    private static void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        
        for (int i = n - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}