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
            SoundManager.Instance?.PlaySound("Click");
            LevelManager.Instance.NextLevel();
            GameManager.Instance.LoadLevel(LevelManager.Instance.GetCurrentLevel());
            gameObject.SetActive(false);
        });
        resetButton?.onClick.AddListener(() =>
        {
            SoundManager.Instance?.PlaySound("Click");
            GameManager.Instance.ResetLevel();
            gameObject.SetActive(false);
        });
        backToMenuButton?.onClick.AddListener(() =>
        {
            SoundManager.Instance?.PlaySound("Click");
            SceneManager.LoadScene("Menu");
        });
    }

}