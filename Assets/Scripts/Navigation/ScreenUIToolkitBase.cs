using UnityEngine;
using UnityEngine.UIElements;

// public enum TransitionType { None, Fade }
public abstract class ScreenUIToolkitBase : ScreenBase {
    protected UIDocument _uiDocument;
    protected VisualElement Root => _uiDocument.rootVisualElement;

    protected override void Awake()
    {
        base.Awake();
        _uiDocument = GetComponent<UIDocument>();
    }

    // public override void OnEnter(object param = null)
    // {
    //     // gameObject.SetActive(true);
    //     _uiDocument.rootVisualElement.SetEnabled(true);
    // }
    // public override void OnExit()
    // {
    //     // gameObject.SetActive(false);    
    //     _uiDocument.rootVisualElement.SetEnabled(false);
    // }
}