using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public Button resetButton;
    public Button menuButton;
    public Canvas levelsCanvas;
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
            if (GridManager.Instance.isFilling) return;
            GameManager.Instance.ResetLevel();
        });

        menuButton.onClick.AddListener(() =>
        {
            // FIX
            levelsCanvas.gameObject.SetActive(true);
        });
    }

    public void OpenConfirmPopup()
    {
        confirmPopup.gameObject.SetActive(true);
    }

   

}