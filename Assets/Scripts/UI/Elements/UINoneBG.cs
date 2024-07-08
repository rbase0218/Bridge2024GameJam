using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINoneBG : MonoBehaviour
{
    public List<GameObject> _objects;

    private int cou = 14;
    public void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     OpenFrame(cou);
        //     cou++;
        // }
    }

    public void OpenFrame(int frameCount)
    {
        foreach(var obj in _objects)
            obj.gameObject.SetActive(false);

        switch (frameCount)
        {
            case 14:
                Open(_objects[0]);
                break;
            case 15:
                Open(_objects[1]);
                break;
            case 20:
                Open(_objects[2]);
                break;
            case 25:
                Open(_objects[3]);
                break;
            case 29:
                Open(_objects[4]);
                break;
            case 38:
                Open(_objects[5]);
                break;
            case 26:
                Open(_objects[6]);
                Open(_objects[8]);
                break;
            case 27:
                Open(_objects[7]);
                Open(_objects[8]);
                break;
            case 18:
                Open(_objects[9]);
                break;
        }
    }

    public TMP_Text currUser;
    public TMP_Text nextUser;
    public void SetFrame25(string currUserText, string targetUser)
    {
        currUser.text = currUserText;
        nextUser.text = targetUser;
    }
    
    public TMP_Text currUser2;
    public TMP_Text nextUser2;
    public void SetFrame15(string currUserText, string targetUser)
    {
        currUser2.text = currUserText;
        nextUser2.text = targetUser;
    }

    private void Open(GameObject obj) => obj.SetActive(true);
}
