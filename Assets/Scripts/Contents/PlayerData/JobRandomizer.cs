using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JobRandomizer
{
    public static void SelectRandomJob(List<UserInfo> players)
    {
        if (players == null || players.Count < 4) 
        {
            Debug.LogError("Error : 플레이어 인원 부족!");
            return;
        }
        
        var jobs = GetJobs(players.Count);
        if (jobs == null)
        {
            Debug.LogError("Error : 직업 목록을 생성할 수 없습니다!");
            return;
        }
        
        ShuffleList(jobs);
        
        for (int i = 0; i < players.Count; i++)
            players[i].jobType = jobs[i];
    }

    public static List<EJobType> GetJobs(int count)
    {
        var jobList = new List<EJobType>();
        
        int vipCount = 0;
        bool needActor = false;
        
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

    private static void ShuffleList<T>(List<T> list)
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