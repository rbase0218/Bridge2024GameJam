using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLayer : MonoBehaviour, ILayoutControl
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