
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject loadingPanel;
    public GameObject actionContainer;
    public GameObject buttonPrefab;
    public GameObject cellPrefab;
    public TextMeshProUGUI textCount;
    public Image targetColor;
    public static GameManager Instance { get; private set; }

    public List<Color> colors;

    [ColorDropdown("colors")]
    public Color target;

    [ColorDropdown("colors")]
    public Color currentColor;
    public int changeCount;


    void Awake()
    {
        Instance = this;
        loadingPanel.SetActive(false);
    }

    void Start()
    {
        LoadLevel(LevelManager.Instance.GetCurrentLevel());
    }

    void LoadButtons()
    {
        actionContainer.transform.ClearChild();

        for (int i = 0; i < colors.Count; i++)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.GetComponent<ChangeColorButton>().color = colors[i];
            button.GetComponent<ChangeColorButton>().SetIndex(i);
            button.transform.SetParent(actionContainer.transform, false);
        }
        actionContainer.transform.GetChild(0).GetComponent<ChangeColorButton>().SetSelected(true);
        currentColor = colors[0];
    }

    public void ReduceChangeCount()
    {
        changeCount--;
        textCount.text = changeCount.ToString();
    }

    public void ResetSelected() // Reset selected buttons
    {
        for (int i = 0; i < actionContainer.transform.childCount; i++)
        {
            var button = actionContainer.transform.GetChild(i);
            button.GetComponent<ChangeColorButton>().SetSelected(false);
        }
    }

    public void LoadLevel(LevelDataSO levelData)
    {
        LevelManager.Instance.current = LevelManager.Instance.levels.IndexOf(levelData);
        var level = levelData.GetLevel();
        GridManager.Instance.Clear();
        GridManager.Instance.rows = level.rows;
        GridManager.Instance.columns = level.columns;

        colors = level.GetColors();
        target = level.GetTarget();
        changeCount = level.maxChange;
        textCount.text = changeCount.ToString();
        targetColor.color = target;


        for (int y = 0; y < level.rows; ++y)
        {
            for (int x = 0; x < level.columns; ++x)
            {
                GameObject cellGO = Instantiate(cellPrefab);
                cellGO.transform.SetParent(GridManager.Instance.transform, false);
                cellGO.name = $"Cell_{x}_{y}";
                Cell cell = cellGO.GetComponent<Cell>();
                cell.position = new Vector2Int(x, y);

                cell.SetColor(level.GetColorAt(x, y));
                int cellValue = level.GetValueAt(x, y);
                // cell.value = cellValue;
                if (cellValue == -1)
                {
                    cell.CanChange = false;

                    // Color cellColor;
                    // ColorUtility.TryParseHtmlString(level.colors[cellValue], out cellColor);
                    // cell.SetColor(cellColor);
                }
                // else
                // {
                //     cell.SetColor(Color.gray);
                // }
                GridManager.Instance.cells.Add(cell.position, cell);
            }
        }
        StartCoroutine(GridManager.Instance.Loading());
        LoadButtons();
    }


    public void CheckWin()
    {
        var cells = GridManager.Instance.GetCells();
        bool isWinner = true;
        foreach (var cell in cells)
        {
            if (!cell.CanChange) continue;
            if (!cell.color.Equals(this.target))
            {
                isWinner = false;
                break;
            }
        }

        if (isWinner)
        {
            UIManager.Instance.OpenConfirmPopup();
            return;
        }

        if (changeCount <= 0)
        {
            print("Thua");
            LoadLevel(LevelManager.Instance.GetCurrentLevel());
            return;
        }
    }

    public void ResetLevel()
    {
        LoadLevel(LevelManager.Instance.GetCurrentLevel());
    }
}