using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultLayer : MonoBehaviour, ILayoutControl,  IUserData
{
    [SerializeField] private TMP_Text introText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text jobText;
    [SerializeField] private TMP_Text descryptionText;
    [SerializeField] private GameObject sameTable;
    
    [SerializeField] private GameObject resultCard;
    
    [SerializeField] private Button nextButton;
    
    public List<UserInfo> Users { get; set; }
    public UserInfo CurtUser { get; set; }

    private EVoteType voteType; 
    
    public void ExitLayout()
    {
        nextButton.onClick.RemoveAllListeners();
        descryptionText.text = "";
        sameTable.SetActive(false);
        introText.gameObject.SetActive(true);
        GetComponent<Button>().interactable = true;
        gameObject.SetActive(false);
    }

    public void StartLayout(List<UserInfo> users, UserInfo curUser)
    {
        Users = users;
        CurtUser = curUser;
        gameObject.SetActive(true);
        nameText.text = TestManager.instance.voteTargetUser.name + "\n\n\n\n\n\n입니다.";
        voteType = TestManager.instance.voteType;
    }

    private void ChangeRole()
    {
        switch (voteType)
        {
            case EVoteType.Same:
                sameTable.SetActive(true);
                break;
            case EVoteType.VIP:
                resultCard.SetActive(true);
                jobText.text = "귀빈";
                descryptionText.text = "앞으로 해당 귀빈은 발언권은\n 잃지만, 지목 투표 권력은\n 유지합니다";
                nextButton.GetComponentInChildren<TMP_Text>().text = "다음 라운드 진행";
                nextButton.onClick.AddListener(() =>
                {
                    TestManager.instance.GoQuestionIntroLayout();
                });
                break;
            case EVoteType.Clown:
                resultCard.SetActive(true);
                jobText.text = "광대";
                descryptionText.text = "그러나 암살자가\n, 본인을 드러내 암구호를\n, 말할 경우, 암살자가\n 승리합니다.";
                
                nextButton.GetComponentInChildren<TMP_Text>().text = "최후 찬스 발동!";
                nextButton.onClick.AddListener(() =>
                {
                    TestManager.instance.GoFinalLayout();
                });
                break;
            case EVoteType.Assassin:
                resultCard.SetActive(true);
                jobText.text = "암살자";
                descryptionText.text = "과연\n, 암살자가\n, 암구호를\n 파악했을까요?";
                nextButton.GetComponentInChildren<TMP_Text>().text = "최후 찬스 발동!";
                nextButton.onClick.AddListener(() =>
                {
                    TestManager.instance.GoFinalLayout();
                });
                break;
            default:
                break;
        }
    }

    public void OnClickImageButton()
    {
        GetComponent<Button>().interactable = false;
        introText.gameObject.SetActive(false);
        ChangeRole();
    }
}
