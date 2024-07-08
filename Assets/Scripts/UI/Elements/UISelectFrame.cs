using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectFrame : MonoBehaviour
{
    public List<GameObject> _objects;

    public void Show(EJobType type)
    {
        foreach(var obj in _objects)
            obj.SetActive(false);

        var result = (type == EJobType.VIP) ? 2 : (type == EJobType.Assassin) ? 0 : 1;
        _objects[result].SetActive(true);
    }
}
