using System;
using System.Collections.Generic;
using UnityEngine;

public class StackNavigator : MonoBehaviour
{


    [Header("Screens")]
    public List<NamedView> screensInScene;
    private Dictionary<string, ScreenBase> _screens;
    private Stack<ScreenBase> _screenStack = new();

    public event Action<string> OnNavigated;

    void Awake()
    {
        _screens = new();
        foreach (var pair in screensInScene)
        {
            _screens[pair.name] = pair.screen;
            // pair.screen.gameObject.SetActive(false); // Tắt tất cả lúc đầu
        }
    }
    void Start()
    {
        //Push first screen active to stack
        foreach (var screen in _screens.Values)
        {
            if (screen.gameObject.activeSelf)
            {
                _screenStack.Push(screen);
                break;
            }
        }
    }

    public void Navigate(string screenName, object param = null)
    {
        if (!_screens.ContainsKey(screenName))
        {
            Debug.LogError($"Screen '{screenName}' không tồn tại.");
            return;
        }

        if (_screenStack.Count > 0)
            _screenStack.Peek().OnExit();

        var screen = _screens[screenName];
        screen.OnEnter(param);
        _screenStack.Push(screen);
        OnNavigated?.Invoke(screenName);
    }

    public void GoBack()
    {
        if (_screenStack.Count <= 1) return;

        var top = _screenStack.Pop();
        top.OnExit();

        var prev = _screenStack.Peek();
        prev.OnEnter();
    }

    public void PopToRoot()
    {
        while (_screenStack.Count > 1)
            GoBack();
    }


}