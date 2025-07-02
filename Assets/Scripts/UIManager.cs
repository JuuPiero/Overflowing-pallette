using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Button resetButton;
    public Button menuButton;
    public Canvas menuCanvas;
    public Canvas confirmPopup;


    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        resetButton.onClick.AddListener(() =>
        {
            SoundManager.Instance?.PlaySound("Click");
            if (GridManager.Instance.isFilling) return;
            GameManager.Instance.ResetLevel();
        });

        menuButton.onClick.AddListener(() =>
        {
            SoundManager.Instance?.PlaySound("Click");
            menuCanvas.gameObject.SetActive(true);
        });
    }

    public void OpenConfirmPopup()
    {
        confirmPopup.gameObject.SetActive(true);
    }

}