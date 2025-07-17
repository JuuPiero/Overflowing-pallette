using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CompletePopup : ScreenUIToolkitBase
{
    public override void OnEnter(object param = null)
    {
        base.OnEnter(param);
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        var yesBtn = Root.QuerySelector<Button>(".yes-btn");
        var resetBtn = Root.QuerySelector<Button>(".reset-btn");
        var quitBtn = Root.QuerySelector<Button>(".quit-btn");

        yesBtn.clicked += () =>
        {
            SoundManager.Instance?.PlaySound("Click");
            LevelManager.Instance.NextLevel();
            GameManager.Instance.LoadLevel(LevelManager.Instance.GetCurrentLevel());
            Navigation.Modal.CloseModal();
        };

        resetBtn.clicked += () =>
        {
            SoundManager.Instance?.PlaySound("Click");
            GameManager.Instance.ResetLevel();
            Navigation.Modal.CloseModal();
        };

        quitBtn.clicked += () =>
        {
            SoundManager.Instance?.PlaySound("Click");
            SceneManager.LoadScene("Menu");
        };
    }
}