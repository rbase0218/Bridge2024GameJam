using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWordCheck : MonoBehaviour
{
    public UIJobGroup _jobGroup;

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
