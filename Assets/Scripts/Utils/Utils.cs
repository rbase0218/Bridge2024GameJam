using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Utils
{
    /// <summary>
    /// 컴포넌트 없으면 Add, 있다면 Get하는 작업을 한 번에 할 수 있도록 하는 함수.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <returns></returns>
    public static T TryOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        go.TryGetComponent(out T component);

        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

    /// <summary>
    /// GetChild를 개선한 함수. recursive는 자식의 자식까지 다 찾을 것인지 묻는 것
    /// </summary>
    /// <param name="go"></param>
    /// <param name="name"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    /// <summary>
    /// FindChild 오버라이딩. 제네릭을 이용한 버전으로 컴포넌트 불러오기 가능.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <param name="name"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    if (transform.TryGetComponent(out T component))
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>(true))
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static T GetDictValue<T>(Dictionary<string, T> dict, string key)
    {
        return dict.TryGetValue(key, out T value) ? value : default(T);
    }

    public static void MakeChildren<T>(Transform go, GameObject child, int count)
    {
        if (count < 0) return;

        for (int i = 0; i < count; ++i)
        {
            var obj = GameObject.Instantiate(child);
            obj.transform.parent = go;
        }
    }

    public static string ChangeEnum(EJobType jobType)
    {
        switch (jobType)
        {
            case EJobType.VIP:
                return "귀빈";
            case EJobType.Assassin:
                return "암살자";
            case EJobType.Actor:
                return "광대";
            default:
                return "귀빈";
        }
    }

    public static void GenerateCardAnim(Transform parent)
    {
        var cardAnimPrefab = Resources.Load("Prefabs/UI/CardAnim") as GameObject;
        if (cardAnimPrefab == null)
        {
            Debug.LogError("CardAnim prefab을 찾을 수 없습니다. 경로: Resources/Prefabs/UI/CardAnim");
            return;
        }
        
        var cardAnim = UnityEngine.Object.Instantiate(cardAnimPrefab, parent);
        cardAnim.transform.localPosition = Vector3.zero;
        cardAnim.transform.localScale = Vector3.one;
        cardAnim.transform.localRotation = Quaternion.identity;
    }
    
    /// <summary>
    /// UnityEvent에 특정 리스너가 있는지 확인하는 헬퍼 메서드 (제네릭 버전)
    /// </summary>
    /// <typeparam name="T">이벤트 파라미터 타입</typeparam>
    /// <param name="unityEvent">확인할 UnityEvent</param>
    /// <param name="listener">찾을 리스너</param>
    /// <returns>리스너가 있으면 true, 없으면 false</returns>
    public static bool HasListener<T>(UnityEvent<T> unityEvent, UnityAction<T> listener)
    {
        for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
        {
            if (unityEvent.GetPersistentMethodName(i) == listener.Method.Name)
            {
                return true;
            }
        }
        return false;
    }
    
    /// <summary>
    /// UnityEvent에 특정 리스너가 있는지 확인하는 헬퍼 메서드 (파라미터 없는 버전)
    /// </summary>
    /// <param name="unityEvent">확인할 UnityEvent</param>
    /// <param name="listener">찾을 리스너</param>
    /// <returns>리스너가 있으면 true, 없으면 false</returns>
    public static bool HasListener(UnityEvent unityEvent, UnityAction listener)
    {
        for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
        {
            if (unityEvent.GetPersistentMethodName(i) == listener.Method.Name)
            {
                return true;
            }
        }
        return false;
    }
    
    /// <summary>
    /// UnityEvent에 특정 메서드명이 포함된 리스너가 있는지 확인하는 헬퍼 메서드
    /// </summary>
    /// <param name="unityEvent">확인할 UnityEvent</param>
    /// <param name="methodName">찾을 메서드명</param>
    /// <returns>리스너가 있으면 true, 없으면 false</returns>
    public static bool HasListenerWithMethodName(UnityEvent unityEvent, string methodName)
    {
        for (int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
        {
            if (unityEvent.GetPersistentMethodName(i).Contains(methodName))
            {
                return true;
            }
        }
        return false;
    }
}