using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIQuestion : MonoBehaviour
{
    public List<GameObject> _objects;
    public UIJobGroup _jobGroup;

    public void OpenFrame(int frameCount)
    {
        foreach(var obj in _objects)
            obj.gameObject.SetActive(false);
        
        switch (frameCount)
        {
            case 21:
                _objects[0].SetActive(true);
                break;
            case 24:
                _objects[1].SetActive(true);
                break;
            case 22:
                _objects[2].SetActive(true);
                break;
            default:
                return;
        }
    }

    public void SetTitle(string currUser)
    {
        _jobGroup.SetText(currUser);
    }

    /// <summary>
    /// Frame 21의 Question 내용을 반환한다.
    /// </summary>
    /// <returns></returns>
    public string GetQuestionText()
    {
        return _objects[0].GetComponentInChildren<InputField>().text;
    }

    /// <summary>
    /// Frame 22의 Question 내용을 작성한다.
    /// </summary>
    /// <param name="text"></param>
    public void SetFrame24(string text)
    {
        _objects[1].GetComponentInChildren<InputField>().text = text;
    }
}
