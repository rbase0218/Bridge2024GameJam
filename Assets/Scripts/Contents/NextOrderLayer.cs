using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextOrderLayer : MonoBehaviour
{
    public void ExitLayout()
    {
        gameObject.SetActive(false);
    }

    public void StartLayout()
    {
        gameObject.SetActive(true);
    }
}