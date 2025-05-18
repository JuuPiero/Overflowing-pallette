
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {
    [SerializeField] private UIDocument _document;
    private VisualElement _root;

    void Awake() {
        _document = GetComponent<UIDocument>();
        if(_document != null) {
            _root = _document.rootVisualElement;
        }
    }

    void Start() {
        var starBtn = _root.GetElementByClassName<Button>("start-btn");
        var exitBtn = _root.GetElementByClassName<Button>("exit-btn");
        starBtn.RegisterCallback<ClickEvent>(e =>
        {
            SceneManager.LoadSceneAsync("Game");
        });
        exitBtn.RegisterCallback<ClickEvent>(e =>
        {
            Application.Quit();
        });
    }
}