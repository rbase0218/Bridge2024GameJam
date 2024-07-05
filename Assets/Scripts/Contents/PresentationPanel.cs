using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresentationPanel : MonoBehaviour
{
    [SerializeField] private Image user1Image;
    [SerializeField] private TMP_Text user1name;
    
    [SerializeField] private Image user2Image;
    [SerializeField] private TMP_Text user2name;
    
    public void SetUserInfo(string user1name, string user2name)
    {
        this.user1name.text = user1name;
        this.user2name.text = user2name;
    }
}
