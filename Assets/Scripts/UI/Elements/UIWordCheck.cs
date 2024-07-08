using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWordCheck : MonoBehaviour
{
    public UIJobGroup _jobGroup;
    public UICardElement _cardElement;
    public UISelectButtons _buttons;

    public List<GameObject> _vipObjs;
    public List<GameObject> _clownObjs;
    public List<GameObject> _assassinObjs;
    
    private void Awake()
    {
        // else if(Input.GetKeyDown(KeyCode.S))
        //     SetLayout(EJobType.Clown);
        // else if(Input.GetKeyDown(KeyCode.D))
        //     SetLayout(EJobType.VIP);
    }

    public void SetLayout(EJobType jobType)
    {
        HideObjects();
        
        switch (jobType)
        {
            case EJobType.VIP:      // 일반
                _jobGroup.SetText(Global.VipJobText);
                ShowObjects(_vipObjs);
                break;
            case EJobType.Clown:    // 광대
                _jobGroup.SetText(Global.ActorJobText);
                ShowObjects(_clownObjs);
                break;
            case EJobType.Assassin: // 암살자
                _jobGroup.SetText(Global.AssJobText);
                ShowObjects(_assassinObjs);
                break;
        }
    }

    /// <summary>
    /// 직업이 암살자가 아닐 경우에만 사용
    /// </summary>
    /// <param name="text"></param>
    public void SetCardData(string text)
    {
        ResetCard();
        _cardElement.SetText(text);
    }

    public void StartGauge()
    {
        UIGauge.instance.SetActive(true);
        UIGauge.instance.SetTime(10f);
        UIGauge.instance.Play();
    }

    public void OpenCard() => _cardElement.OpenCard();

    public void ResetCard() => _cardElement.Reset();
    
    public void SetData(params string[] names) => _buttons.SetData(names);
    
    public void SetTitle(string txt) => _jobGroup.SetText(txt);

    private void ShowObjects(List<GameObject> objs, bool isShow = true)
    {
        foreach (var obj in objs)
        {
            obj.SetActive(isShow);
        }
    }
    private void HideObjects()
    {
        ShowObjects(_vipObjs, false);
        ShowObjects(_assassinObjs, false);
        ShowObjects(_clownObjs, false);
    }
}
