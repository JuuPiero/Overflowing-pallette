using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Cell : MonoBehaviour {

    [SerializeField] private GameObject _icon;
    [SerializeField] private Animator _anim;

    public Vector2Int position;

    [ColorDropdown]
    public Color color;

    [SerializeField] private bool _canChange = true;
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
    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _anim = GetComponent<Animator>();
    }

    void OnValidate()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _anim = GetComponent<Animator>();
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
        else
        {
            _icon.SetActive(false);
        }
    }
  
    void Start() {
        _button.onClick.AddListener(() => {
            GameManager.Instance.gridManager?.Fill(position.x, position.y, GameManager.Instance.CurrentColor);
        });
    }

    public void ChangeColor(Color color)
    {
        if (!_canChange) return;
        SetColor(color);
        _anim?.SetTrigger("Pressed");
        SoundManager.Instance?.PlaySound("Fill");
    }
    public void SetColor(Color color)
    {
        this.color = color;
        _image.color = this.color;
    }

    public Color GetColor() => _image.color;
}
