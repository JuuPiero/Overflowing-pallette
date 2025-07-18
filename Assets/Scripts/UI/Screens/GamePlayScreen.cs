using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GamePlayScreen : ScreenBase
{

    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _resetButton;

    [SerializeField] private GameObject _actionContainer;
    [SerializeField] private GameObject _colorButtonPrefab;

    [SerializeField] private TMP_Text _changeCountText;


    protected void Start()
    {
        GameManager.Instance.OnLevelChanged += ResetUI;
        GameManager.Instance.OnChooseColorChanged += ResetSelected;
        GameManager.Instance.gridManager.OnFilled += UpdateChangeCountText;
        ResetUI();

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

    private void UpdateChangeCountText()
    {
        _changeCountText.text = GameManager.Instance.changeCount.ToString();
    }

    private void ResetUI()
    {
        _actionContainer.transform.ClearChild();
        var colors = GameManager.Instance.colors;

        for (int i = 0; i < colors.Count; i++)
        {
            GameObject button = Instantiate(_colorButtonPrefab);
            button.GetComponent<ChangeColorButton>().color = colors[i];
            button.GetComponent<ChangeColorButton>().SetIndex(i);
            button.transform.SetParent(_actionContainer.transform, false);
        }
        _actionContainer.transform.GetChild(0).GetComponent<ChangeColorButton>().SetSelected(true);
        UpdateChangeCountText();
    }

    public void ResetSelected() // Reset selected buttons
    {
        for (int i = 0; i < _actionContainer.transform.childCount; i++)
        {
            var button = _actionContainer.transform.GetChild(i);
            button.GetComponent<ChangeColorButton>().SetSelected(false);
        }
    }
}