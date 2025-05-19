
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorButton : MonoBehaviour
{
    private Image _image;
    private Button _button;
    private Outline _border;
    [SerializeField] private TextMeshProUGUI _text;
    public Color color;

    public bool selected = false;

    void Awake()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _border = GetComponent<Outline>();
   
    }

    void Start() {
        _image.color = color;

        _button.onClick.AddListener(() => {

            SoundManager.Instance?.PlaySound("Click");

            GameManager.Instance.ResetSelected();
            GameManager.Instance.currentColor = color;
            SetSelected(true);
        });
    }
    public void SetIndex(int index) {
        _text.text = (index + 1).ToString();
    }

    public void SetSelected(bool selected) {
        _border.enabled = selected;
    }
}
