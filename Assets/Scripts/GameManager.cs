
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
    public TMP_Text textCount;
    public Image targetColor;
    public static GameManager Instance { get; private set; }

    public GridManager gridManager;

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
        gridManager.Clear();
        gridManager.rows = level.rows;
        gridManager.columns = level.columns;

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
                cellGO.transform.SetParent(gridManager.transform, false);
                cellGO.name = $"Cell_{x}_{y}";
                Cell cell = cellGO.GetComponent<Cell>();
                cell.position = new Vector2Int(x, y);

                cell.SetColor(level.GetColorAt(x, y));
                int cellValue = level.GetValueAt(x, y);
                if (cellValue == -1)
                {
                    cell.CanChange = false;
                }
                gridManager.cells.Add(cell.position, cell);
            }
        }
        StartCoroutine(gridManager.Loading());
        LoadButtons();
    }


    public void CheckWin()
    {
        var cells = gridManager.GetCells();
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
            Navigation.Modal.ShowModal("CompleteModal");
            return;
        }

        if (changeCount <= 0)
        {
            ResetLevel();
            return;
        }
    }

    public void ResetLevel()
    {
        LoadLevel(LevelManager.Instance.GetCurrentLevel());
    }
}