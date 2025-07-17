using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class LevelPopup : ScreenUIToolkitBase
{
    [SerializeField] private VisualTreeAsset _levelButtonTemplate;

    public override void OnEnter(object param = null)
    {
        base.OnEnter(param);
        LoadData();
    }
  

    private void LoadData()
    {
        var levelContainer = Root.QuerySelector<VisualElement>(".list-levels");
        levelContainer?.Clear();
        var levels = LevelManager.Instance?.levels;

        for (int i = 0; i < levels.Count; i++)
        {
            var levelButtonInstance = _levelButtonTemplate.Instantiate();
            var levelButton = levelButtonInstance.Q<Button>();
            levelButton.text = (i + 1).ToString();

            int levelIndex = i; // tránh closure 
            levelButton.clicked += () => OnLevelButtonClicked(levelIndex);
            levelContainer.Add(levelButtonInstance);
        }


        var closeBtn = Root.QuerySelector<Button>(".close-btn");
        closeBtn.clicked += () =>
        {
            SoundManager.Instance?.PlaySound("Click");
            Navigation.Modal.CloseModal();
        };
    }

    private void OnLevelButtonClicked(int levelIndex)
    {
        SoundManager.Instance?.PlaySound("Click");
        LevelManager.Instance.current = levelIndex;
        SceneManager.LoadScene("Game");
    }
  
}