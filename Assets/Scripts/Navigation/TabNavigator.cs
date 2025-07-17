using System.Collections.Generic;
using UnityEngine;

public class TabNavigator : MonoBehaviour {

    [Header("Tab Screens")]
    public List<NamedView> tabsInScene;

    private Dictionary<string, ScreenBase> _tabs;
    [SerializeField] private string _currentTab = null;

    void Awake() {

        _tabs = new();
        foreach (var tab in tabsInScene) {
            _tabs[tab.name] = tab.screen;
            tab.screen.gameObject.SetActive(false);
        }

        // Hiển thị tab đầu tiên mặc định
        if (tabsInScene.Count > 0) {
            _currentTab = tabsInScene[0].name;
            SwitchTab(tabsInScene[0].name);
        }
    }

    public void SwitchTab(string tabName) {
        if (!_tabs.ContainsKey(tabName)) {
            Debug.LogError($"Tab '{tabName}' không tồn tại.");
            return;
        }

        // Ẩn tab hiện tại
        if (!string.IsNullOrEmpty(_currentTab))
        {
            // print(_currentTab);
            _tabs[_currentTab].OnExit();
        }

        // Hiện tab mới
        _currentTab = tabName;
        _tabs[_currentTab].OnEnter();
    }

    public string GetCurrentTab() => _currentTab;
}