using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCondition : MonoBehaviour
{
    public TMP_Text text;

    public void Awake()
    {
        GetComponent<Button>().onClick.AddListener(SetClick);
    }

    private void SetClick()
    {
        RoundManager.instance.OffAllFrame();
        RoundManager.instance.uiResult.gameObject.SetActive(true);

        if (text.text == Global.AssJobText)
        {
            RoundManager.instance.uiResult.OpenFrameA(32);
            
        } else if (text.text == Global.ActorJobText)
        {
            RoundManager.instance.uiResult.OpenFrameA(36);
            var user = RoundManager.instance.GetCurrentUser();
            RoundManager.instance.uiResult.SetFrame31353637(user.name, user.jobType);
        } else if (text.text == Global.VipJobText)
        {
            RoundManager.instance.uiResult.OpenFrameA(35);
            var user = RoundManager.instance.GetCurrentUser();
            RoundManager.instance.uiResult.SetFrame31353637(user.name, user.jobType);
        }
    }
}
