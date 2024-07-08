using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIThis : MonoBehaviour
{
    public List<GameObject> _objects;
    
    // Frame 8
    public UIJobGroup _frame8;
    // Frame 9
    public UIJobGroup _frame9Job;
    public UICardElement _frame9Card;
    // Frame 11
    public UIJobGroup _frame11;

    public void OpenFrame(int count)
    {
        foreach(var obj in _objects)
            obj.SetActive(false);
        
        if(count == 8)
            _objects[0].SetActive(true);
        else if(count == 9)
            _objects[1].SetActive(true);
        else if(count == 11)
            _objects[2].SetActive(true);
    }
}
