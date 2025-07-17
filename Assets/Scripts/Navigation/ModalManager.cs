using System.Collections.Generic;
using UnityEngine;

public class ModalManager : MonoBehaviour {
    

    [Header("Modals in Scene")]
    public List<NamedView> modalsInScene;

    // [Header("References")]
    // public Transform modalContainer;

    private Dictionary<string, ScreenBase> _modals;
    private Stack<ScreenBase> _modalStack = new();

    void Awake() {

        _modals = new();
        foreach (var pair in modalsInScene)
        {
            _modals[pair.name] = pair.screen;
            // pair.screen.gameObject.SetActive(false);
            // pair.screen.OnExit();
        }
    }

    public void ShowModal(string name) {
        if (!_modals.ContainsKey(name)) {
            Debug.LogError($"Modal '{name}' không tồn tại.");
            return;
        }

        var modal = _modals[name];
        // modal.transform.SetParent(modalContainer, false);
        modal.OnEnter();
        _modalStack.Push(modal);
    }

    public void Show(ScreenBase modal) {
        modal?.OnEnter();
    }

    public void CloseModal() {
        if (_modalStack.Count == 0) return;

        var top = _modalStack.Pop();
        top.OnExit();
    }
    public void Close(ScreenBase modal) {
        modal?.OnExit();
    }

    public T Get<T>() where T : ScreenBase
    {
        foreach (var modal in _modals.Values)
        {
            if (modal is T typed) return typed;
        }
        Debug.LogError($"Modal of type {typeof(T).Name} not found.");
        return null;
    }

    

}