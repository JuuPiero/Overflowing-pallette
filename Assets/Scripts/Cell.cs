using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Cell : MonoBehaviour {
    private GameManager _gameManager;

    [SerializeField] private GameObject _icon;


    public Vector2Int position;

    [ColorDropdown("_gameManager.colors")]
    public Color color;

    public int value;
    
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
    void Awake()
    {
        _image = GetComponent<Image>(); 
        _button = GetComponent<Button>();
    }

    void OnValidate()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _image = GetComponent<Image>();
        if (color != null)
        {
            ChangeColor(color);
        }


        //------------------- x
        if (!_canChange)
        {
            _image.color = Color.gray;
            _icon.SetActive(true);
        }
    }

  
    void Start() {
        _button.onClick.AddListener(() => {
            GridManager.Instance?.Fill(position.x, position.y, GameManager.Instance.currentColor);
        });
    }

    public void ChangeColor(Color color) {
        if(!_canChange) return;
        this.color = color;
        _image.color = this.color;
    }
    public void SetColor(Color color)
    {
        this.color = color;
        _image.color = this.color;
    }

    public Color GetColor() => _image.color;
}
