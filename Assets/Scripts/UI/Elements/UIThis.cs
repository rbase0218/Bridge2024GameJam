using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIThis : MonoBehaviour
{
    public List<GameObject> _objects;
    
    // Frame 8
    public UIJobGroup _frame8;
    // Frame 9
    public UIJobGroup _frame9Job;
    public UICardElement _frame9Card;
    public UIImageSwap _frame9Swap;
    // Frame 11
    public UIJobGroup _frame11;

    // private void Awake()
    // {
    //     OpenFrame(11);
    //     SetFrame11("명수");
    // }

    /// <summary>
    /// 사용할 프레임 수 매개변수 입력
    /// </summary>
    /// <param name="count"></param>
    public void OpenFrame(int count)
    {
        foreach(var obj in _objects)
            obj.SetActive(false);
        
        if(count == 8)
            _objects[0].SetActive(true);
        else if(count == 9)
            _objects[1].SetActive(true);
        else if(count == 11)
            _objects[2].SetActive(true);
    }

    /// <summary>
    /// 현재 유저의 이름 매개변수
    /// </summary>
    /// <param name="name"></param>
    public void SetFrame8(string name)
    {
        _frame8.SetText(name);
    }

    /// <summary>
    /// 직업까지 매개변수 입력
    /// </summary>
    /// <param name="name"></param>
    /// <param name="jobType"></param>
    public void SetFrame9(string name, EJobType jobType)
    {
        _frame9Job.SetText(name);
        
        var jobText = (jobType == EJobType.Assassin) ? Global.AssJobText :
            (jobType == EJobType.Clown) ? Global.ActorJobText : Global.VipJobText;
        _frame9Swap.SetImage(jobType);
        _frame9Card.SetText(jobText);
    }

    /// <summary>
    /// 카드 오픈
    /// </summary>
    public void OpenFrame9()
    {
        _frame9Card.OpenCard();
    }

    /// <summary>
    /// 이번 라운드의 인질의 이름ㅈ
    /// </summary>
    /// <param name="nextName"></param>
    public void SetFrame11(string nextName)
    {
        _frame11.SetText(nextName);
    }
}
