using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuScreen : ScreenUIToolkitBase
{
    private void Start()
    {
        var starBtn = Root?.GetElementByClassName<Button>("start-btn");
        var exitBtn = Root?.GetElementByClassName<Button>("exit-btn");
        var levelsBtn = Root?.GetElementByClassName<Button>("levels-btn");


        if (LevelManager.Instance?.current > 0)
        {
            starBtn.text = "Continue";
        }


        starBtn.RegisterCallback<ClickEvent>(e =>
        {
            SoundManager.Instance?.PlaySound("Click");
            SceneManager.LoadSceneAsync("Game");
        });
        exitBtn.RegisterCallback<ClickEvent>(e =>
        {
            SoundManager.Instance?.PlaySound("Click");
            Application.Quit();
        });

        levelsBtn.clicked += () =>
        {
            // print("ấn nút mở levels");
            SoundManager.Instance?.PlaySound("Click");
            Navigation.Modal.ShowModal("LevelPopup");
        };
    }
}