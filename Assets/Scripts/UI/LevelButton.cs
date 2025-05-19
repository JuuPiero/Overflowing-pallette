using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] public TextMeshProUGUI _label;
    [SerializeField] private LevelDataSO _levelData;
    public bool isCurrentLevel = false;
    void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void SetLevel(LevelDataSO levelData)
    {
        _levelData = levelData;
    }
    public void SetLabel(string label)
    {
        _label.text = label;
    }
    void Start()
    {
        _button.onClick.AddListener(() =>
        {
            SoundManager.Instance?.PlaySound("Click");

            if (GameManager.Instance != null)
                GameManager.Instance.LoadLevel(_levelData);
            else
                LevelManager.Instance.current = LevelManager.Instance.levels.IndexOf(_levelData);
                SceneManager.LoadScene("Game");
                // GameManager.Instance.LoadLevel(_levelData);
        });
    }

 
}
