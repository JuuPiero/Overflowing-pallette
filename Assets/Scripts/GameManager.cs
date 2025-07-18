
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action OnLevelChanged;
    public event Action OnChooseColorChanged;


    public GameObject cellPrefab;
    public Image targetColor;

    public GridManager gridManager;

    public List<Color> colors;

    [ColorDropdown()]
    public Color target;

    [ColorDropdown()]
    [SerializeField] private Color _currentColor;
    public Color CurrentColor
    {
        get => _currentColor;
        set
        {
            _currentColor = value;
            OnChooseColorChanged?.Invoke();
        }
    }
    public int changeCount;


    void Awake()
    {
        Instance = this;
        LoadLevel(LevelManager.Instance.GetCurrentLevelSO());
    }


    public void ReduceChangeCount()
    {
        changeCount--;
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
        CurrentColor = colors[0];
        OnLevelChanged?.Invoke();
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
        LoadLevel(LevelManager.Instance.GetCurrentLevelSO());
    }
}