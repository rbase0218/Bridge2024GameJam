using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JobOpenLayer : MonoBehaviour, ILayoutControl
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text jobText;
    [SerializeField] private GameObject card;
    [SerializeField] private Button cardButton;
    [SerializeField] private TMP_Text jobDescryptionText;
    [SerializeField] private Sprite[] jobSprites;
    [SerializeField] private Image jobImage;
    private UserInfo curUser;
    
    public void ExitLayout()
    {
        cardButton.interactable = false;
        card.SetActive(true);
        gameObject.SetActive(false);
        jobDescryptionText.gameObject.SetActive(false);
        this.curUser.myTurn = false;
    }

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        nameText.text = curUser.name;
        jobText.text = Utils.ChangeEnum(curUser.jobType);
        ChangeDescryption(curUser.jobType);
        gameObject.SetActive(true);
        this.curUser = curUser;
        this.curUser.myTurn = true;
        
    }

    public void OnClickSendButton()
    {
        cardButton.interactable = true;
        jobDescryptionText.gameObject.SetActive(true);
        card.SetActive(false);
    }

    private void ChangeDescryption(EJobType jobType)
    {
        switch (jobType)
        {
            case EJobType.VIP:
                jobDescryptionText.text = "무도회에 침입한\n 불청객 암살자를 색출하면\n 승리합니다.";
                jobImage.sprite = jobSprites[0];
                break;
            case EJobType.Clown:
                jobDescryptionText.text = "무도회에 잠입한\n 암살자로 지목당하면\n 승리합니다.";
                jobImage.sprite = jobSprites[1];
                break;
            case EJobType.Assassin:
                jobDescryptionText.text = "무도회의 게임에 참여한\n 모든 귀빈들을 인질로 잡거나,\n 그들의 암구호를 맞추면\n 승리합니다.";
                jobImage.sprite = jobSprites[2];
                break;
            default:
                break;
        }
    }
}   