using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private WindowContainer _windowContainer;
    private Stack<UIWindow> _activeWindowStack;

    public void Init()
    {
        // 모든 Window의 인스턴스를 저장하고 관리한다.
        _windowContainer = new WindowContainer();
        
        // 현재 활성화된 Window의 인스턴스를 저장하고 관리한다.
        _activeWindowStack = new Stack<UIWindow>();
    }

    public bool RegisterWindow(UIWindow window)
    {
        return _windowContainer.TryAdd(window.GetType(), window);
    }

    public T ShowWindow<T>() where T : UIWindow
    {
        var findWindow = _windowContainer.GetWindow<T>();
        if (findWindow == null) { Debug.Log("존재하지 않는 Window를 실행했습니다."); return null; }
        
        _activeWindowStack.Push(findWindow);
        findWindow.Open();
        return findWindow;
    }
    
    public T PeakWindow<T>() where T : UIWindow
    {
        return _activeWindowStack?.Peek() as T;
    }

    public void CloseWindow()
    {
        if (_activeWindowStack.Count > 0)
        {
            var window = _activeWindowStack?.Pop();
            window.Hide();
        }
    }
}
