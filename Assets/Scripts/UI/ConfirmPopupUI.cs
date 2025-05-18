using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConfirmPopupUI : MonoBehaviour
{
    public Button yesButton;
    public Button resetButton;
    public Button backToMenuButton;

    void Start()
    {
        yesButton?.onClick.AddListener(() =>
        {
            LevelManager.Instance.NextLevel();
            GameManager.Instance.LoadLevel(LevelManager.Instance.GetCurrentLevel());
            gameObject.SetActive(false);
        });
        resetButton?.onClick.AddListener(() =>
        {
            GameManager.Instance.ResetLevel();
            gameObject.SetActive(false);
        });
        backToMenuButton?.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Menu");
        });
    }

}