
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {
    [SerializeField] private UIDocument _document;
    [SerializeField] private GameObject levelsCanvas;

    private VisualElement _root;

    void Awake() {
        _document = GetComponent<UIDocument>();
        _root = _document?.rootVisualElement;
    }

    void Start()
    {

        var starBtn = _root?.GetElementByClassName<Button>("start-btn");
        var exitBtn = _root?.GetElementByClassName<Button>("exit-btn");
        var levelsBtn = _root?.GetElementByClassName<Button>("levels-btn");

        if (LevelManager.Instance?.current > 0)
        {
            starBtn.text = "▶ Continue";
        }


        starBtn.RegisterCallback<ClickEvent>(e =>
        {
            SceneManager.LoadSceneAsync("Game");
        });
        exitBtn.RegisterCallback<ClickEvent>(e =>
        {
            Application.Quit();
        });

        levelsBtn.RegisterCallback<ClickEvent>(e =>
        {
            levelsCanvas.SetActive(true);
        });
    }
}