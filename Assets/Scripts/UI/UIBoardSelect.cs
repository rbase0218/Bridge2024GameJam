using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBoardSelect : MonoBehaviour
{
    public List<GameObject> objs;
    public int count = 0;

    public void SetBoard(int num)
    {
        count += num;
        
        if (count < 0)
            count = 0;
        if (count > objs.Count - 1)
            count = objs.Count - 1;

        for (int i = 0; i < objs.Count; ++i)
        {
            if (count == i)
            {
                objs[i].SetActive(true);
            }
            else
                objs[i].SetActive(false);
        }
    }
}
