using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GamePlayScreen : ScreenBase
{
    
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _resetButton;

    // [SerializeField] private GameObject _actionContainer;
    // [SerializeField] private GameObject _changeColorButtonPrefab;
    // [SerializeField] private TMP_Text _changeCountText;
    

    protected void Start()
    {
        _menuButton.onClick.AddListener(() =>
        {
            SoundManager.Instance?.PlaySound("Click");
            Navigation.Modal.ShowModal("GameMenuPopup");
        });
        _resetButton.onClick.AddListener(() =>
        {
            SoundManager.Instance?.PlaySound("Click");
            if (GameManager.Instance.gridManager.isFilling) return;
            GameManager.Instance.ResetLevel();
        });
    }
}