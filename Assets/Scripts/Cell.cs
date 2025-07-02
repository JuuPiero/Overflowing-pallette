using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Cell : MonoBehaviour {

    [SerializeField] private GameObject _icon;

    public Vector2Int position;

    [ColorDropdown("_gameManager.colors")]
    public Color color;

    private bool _canChange = true;
    public bool CanChange
    {
        get { return _canChange; }
        set
        {
            _canChange = value;
            _icon.SetActive(!_canChange);
        }
    }
    private Image _image;
    private Button _button;
    void Awake()
    {
        _image = GetComponent<Image>(); 
        _button = GetComponent<Button>();
    }

    void OnValidate()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        if (color != null)
        {
            ChangeColor(color);
        }

        if (!_canChange)
        {
            _image.color = Color.gray;
            _icon.SetActive(true);
            _button.transition = Selectable.Transition.ColorTint;
        }
    }
  
    void Start() {
        _button.onClick.AddListener(() => {
            GridManager.Instance?.Fill(position.x, position.y, GameManager.Instance.currentColor);
        });
    }

    public void ChangeColor(Color color)
    {
        if (!_canChange) return;
        SetColor(color);
        SoundManager.Instance?.PlaySound("Fill");
    }
    public void SetColor(Color color)
    {
        this.color = color;
        _image.color = this.color;
    }

    public Color GetColor() => _image.color;
}
