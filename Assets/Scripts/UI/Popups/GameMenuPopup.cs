using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameMenuPopup : ScreenUIToolkitBase
{
    public override void OnEnter(object param = null)
    {
        base.OnEnter(param);
        Init();
    }

    void Init()
    {
        var continueBtn = Root.QuerySelector<Button>(".continue-btn");
        var levelBtn = Root.QuerySelector<Button>(".level-btn");
        var quitBtn = Root.QuerySelector<Button>(".quit-btn");

        continueBtn.clicked += () =>
        {
            SoundManager.Instance?.PlaySound("Click");
            Navigation.Modal.CloseModal();
        };

        levelBtn.clicked += () =>
        {
            SoundManager.Instance?.PlaySound("Click");
            Navigation.Modal.CloseModal();
            Navigation.Modal.ShowModal("LevelPopup");
        };

        quitBtn.clicked += () =>
        {
            SoundManager.Instance?.PlaySound("Click");
            Navigation.Modal.CloseModal();
            SceneManager.LoadScene("Menu");
        };
    }
}