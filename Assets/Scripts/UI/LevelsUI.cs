using UnityEngine;
using UnityEngine.UI;

public class LevelsUI : MonoBehaviour
{
    public GameObject levelsContainer;
    public GameObject levelButtonPrefab;

    public Button closeButton;

    void Start()
    {
        LoadLevels();
        closeButton?.onClick.AddListener(() =>
        {
            SoundManager.Instance?.PlaySound("Click");
            gameObject.SetActive(false);
        });
    }

    public void LoadLevels() {
        levelsContainer.transform.ClearChild();
        var levels = LevelManager.Instance?.levels;

        for (int i = 0; i < levels.Count; i++)
        {
            GameObject button = Instantiate(levelButtonPrefab);
            button.transform.SetParent(levelsContainer.transform, false);
            LevelButton levelButton = button.GetComponent<LevelButton>();
            levelButton.SetLevel(levels[i]);
            levelButton.SetLabel((i + 1).ToString());
            levelButton.isCurrentLevel = LevelManager.Instance.current == i;
        }
    }
}