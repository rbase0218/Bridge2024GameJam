using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardButton : MonoBehaviour
{
    public void OnClick()
    {
        if(gameObject.activeInHierarchy)
            gameObject.SetActive(false);
        else
        {
            gameObject.SetActive(true);
        }
    }
}