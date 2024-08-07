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
        var window = _windowContainer?.GetWindow<T>();
        if (window == null)
        {
            Debug.Log("존재하지 않는 Window를 실행했습니다.");
            return null;
        }

        // 항상 열려있는 Window가 아닐 경우, Stack에 추가한다.
        if (window.IsAlwaysOpen == false)
            _activeWindowStack?.Push(window);
        
        window.Open();
        return window;
    }

    // 모든 Window를 담고 있는 Container에서 Instance를 전달한다.
    public T GetWindow<T>() where T : UIWindow
    {
        return _windowContainer?.GetWindow<T>();
    }
    
    // 활성화된 Window의 마지막을 전달한다.
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
