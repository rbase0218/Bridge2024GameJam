using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIResult : MonoBehaviour
{
    public List<GameObject> _resultType;
    public List<GameObject> _aTypeObjects;
    public List<GameObject> _bTypeObjects;

    public void Awake()
    {
        OpenType(false);
        OpenFrameA_BG_Frame(EJobType.Clown);
        OpenFrameA(31);
        
        SetFrame31353637("누굴", EJobType.Clown);
    }

    public void OpenType(bool type)
    {
        _resultType[System.Convert.ToInt32(type)].SetActive(true);
        _resultType[System.Convert.ToInt32(!type)].SetActive(false);
    }

    public UISelectFrame _selectFrame;
    public void OpenFrameA_BG_Frame(EJobType jobType) => _selectFrame.Show(jobType);
    
    public void OpenFrameA(int frameCount)
    {
        foreach(var obj in _aTypeObjects)
            obj.SetActive(false);

        switch (frameCount)
        {
            case 30:
                Open(_aTypeObjects[0]);
                break;
            case 31:
                Open(_aTypeObjects[1]);
                break;
            case 32:
                Open(_aTypeObjects[2]);
                break;
            case 33:
                Open(_aTypeObjects[3]);
                break;
            case 34:
                Open(_aTypeObjects[3]);
                break;
            case 35:
                Open(_aTypeObjects[1]);
                break;
            case 36:
                Open(_aTypeObjects[1]);
                break;
            case 37:
                Open(_aTypeObjects[1]);
                break;
        }
        
    }

    public void OpenFrameB(int frameCount)
    {
        foreach(var obj in _bTypeObjects)
            obj.SetActive(false);

        switch (frameCount)
        {
            case 39:
                Open(_bTypeObjects[0]);
                break;
            case 40:
                Open(_bTypeObjects[0]);
                break;
            case 41:
                Open(_bTypeObjects[0]);
                break;
            case 42:
                Open(_bTypeObjects[0]);
                break;
            case 43:
                Open(_bTypeObjects[1]);
                break;
        }
        
    }
    
    // Frame 30
    public void SetFrame30(string targetName)
    {
        var get = _aTypeObjects[0].GetComponentInChildren<UIJobGroup>().text.text;
        var result = get.Replace("김OO", targetName);
        _aTypeObjects[0].GetComponentInChildren<UIJobGroup>().SetText(result);
    }
    
    // Frame 31, 35, 36, 37
    public UIJobGroup header;
    public UIImageSwap imageSwap;
    public UIJobGroup job;
    public TMP_Text infoText;
    public UIJobGroup button;
    private readonly string accText = "과연\n암살자가 암구호를\n파악했을까요?";
    private readonly string vipText = "앞으로 해당 귀빈은\n발언권은 없지만\n투표 권력은 유지합니다.";
    private readonly string actorText = "그러나 암살자가\n본인을 드러내 암구호를 외친다면,\n암살자가 승리합니다.";
    private readonly string winActorText = "뜻밖의 광대가\n귀빈들과의 게임에서\n승리를 가져갑니다.";
    public void SetFrame31353637(string targetName, EJobType type, bool isTrigger = false)
    {
        if (isTrigger)
        {
            header.SetText("오답입니다!");
            imageSwap.SetImage(EJobType.Clown);
            job.SetText(Global.ActorJobText);
            infoText.text = winActorText;
            button.SetText("최종 결과 확인");
            
            return;
        }
        
        header.SetText(targetName + "은");
        imageSwap.SetImage(type);
        var resultJobText = (type == EJobType.Assassin) ? Global.AssJobText :
            (type == EJobType.Clown) ? Global.ActorJobText : Global.VipJobText;
        job.SetText(resultJobText);
        infoText.text = (type == EJobType.Assassin) ? accText : (type == EJobType.VIP) ? vipText : actorText;
        var resultButtonText = (type == EJobType.Assassin) ? "최후 찬스 발동!" :
            (type == EJobType.VIP) ? "다음 라운드 진행" : "최후 찬스 발동!";
        button.SetText(resultButtonText);

    }
    
    // Frame 33
    private readonly string winAssText = "암살자가\n귀빈들과의 대결에서\n승리했습니다.";
    private readonly string loseVIPText = "귀빈들이\n그들의 무도회를\n지켜냈습니다.";
    public UIImageSwap swap;
    public UIJobGroup group;
    public TMP_Text header2;
    public TMP_Text infotext2;
    public void SetFrame3334(EJobType type)
    {
        if (type == EJobType.VIP)
        {
            swap.SetImage(type);
            group.SetText(Global.VipJobText);
            header2.text = "오답입니다!";
            infotext2.text = loseVIPText;
        }
        else if (type == EJobType.Assassin)
        {
            swap.SetImage(type);
            group.SetText(Global.AssJobText);
            header2.text = "정답입니다!";
            infotext2.text = winAssText;
        }
    }
    
    // Frame 39, 40, 41, 42
    public List<GameObject> _nameBoxList;
    public GameObject picture_A;
    public GameObject picture_B;
    public void SetFrame39404142(params string[] names)
    {
        foreach (var nameobx in _nameBoxList)
            nameobx.SetActive(false);
        
        if(names.Length % 2 == 0)
        {
            picture_A.SetActive(false);
            picture_B.SetActive(true);
        }
        else
        {
            picture_A.SetActive(true);
            picture_B.SetActive(false);
        }

        for (int i = 0; i < names.Length; ++i)
        {
            _nameBoxList[i].GetComponentInChildren<TMP_Text>().text = names[i];
            _nameBoxList[i].gameObject.SetActive(true);
        }
    }

    public UICardElement card;
    public void SetFrame43(string text)
    {
        card.Reset();
        card.SetText(text);
    }

    public void OpenCard()
    {
        card.OpenCard();
    }

    private void Open(GameObject obj) => obj.SetActive(true);

}
