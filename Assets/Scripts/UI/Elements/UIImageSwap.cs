using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageSwap : MonoBehaviour
{
    private Image image;
    public List<Sprite> _sprs;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetImage(EJobType type)
    {
        if (type == EJobType.VIP)
            image.sprite = _sprs[0];
        else if (type == EJobType.Assassin)
            image.sprite = _sprs[1];
        else if (type == EJobType.Clown)
            image.sprite = _sprs[2];
    }
}
