using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class GridManager : MonoBehaviour {

    public static GridManager Instance { get; private set; }
    // public event Action OnColorChange;


    public float cellSize;
    public int rows;
    public int columns;

    private List<Cell> _cells = new();
    public bool isLoading = true;    
    public bool isFilling = false;

    private GridLayoutGroup _layout;

    void Awake()
    {
        Instance = this;
    }

    // void Start()
    // {
        
    //     // StartCoroutine(Loading());
    // }
    void OnValidate() {
        // Setup();
    }

    public IEnumerator Loading() {
        isLoading = true;
        yield return new WaitForSeconds(0.3f);
        Setup();
        isLoading = false;
        // if(!isLoading) {
        //     GameManager.Instance?.loadingPanel.SetActive(false);
        // }
    }

    public void Fill(int x, int y, Color colorTochange)
    {
        var targetCell = GetCell(x, y);
        if (isFilling || !targetCell.CanChange) return;
        if (colorTochange.Equals(targetCell.color)) return;
        GameManager.Instance?.ReduceChangeCount();
        StartCoroutine(ChangeColor(x, y, colorTochange));
    }

    public IEnumerator ChangeColor(int x, int y, Color colorTochange)
    {
        var targetCell = GetCell(x, y);
        Color prevColor = targetCell.color;
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        queue.Enqueue(targetCell.position);
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        Vector2Int[] directions = {
            new Vector2Int(-1, 0),
            new Vector2Int(1, 0),
            new Vector2Int(0, -1),
            new Vector2Int(0, 1),
        };
        isFilling = true;
        while (queue.Count > 0)
        {
            Vector2Int position = queue.Dequeue();
            if (visited.Contains(position)) continue;
            visited.Add(position);
            var cell = GetCell(position.x, position.y);
            if (cell != null && cell.color.Equals(prevColor))
            {
                cell?.ChangeColor(colorTochange);
                yield return new WaitForSeconds(0.06f);
            }

            foreach (var dir in directions)
            {
                Vector2Int newPos = new Vector2Int(position.x + dir.x, position.y + dir.y);
                if (newPos.x >= 0 && newPos.x < this.columns &&
                   newPos.y >= 0 && newPos.y < this.rows &&
                   !visited.Contains(newPos) &&
                    GetCell(newPos.x, newPos.y).color.Equals(prevColor)
                )
                {
                    queue.Enqueue(newPos);
                }
            }
        }
        isFilling = false;
        GameManager.Instance.CheckWin();
    }

    public Cell GetCellByIndex(int index) {
        return transform.GetChild(index).GetComponent<Cell>();
    }

    public List<Cell> GetCells() {
        int count = transform.childCount;
        List<Cell> cells = new();
        for (int i = 0; i < count; i++)
        {
            cells.Add(transform.GetChild(i).GetComponent<Cell>());
        }
        return cells;
    }

    public Cell GetCell(int x, int y) {
        return GetCell(new Vector2Int(x, y));
    }
    public Cell GetCell(Vector2Int position) {
        if(position.x < 0 || position.y < 0 || position.x >= columns || position.y >= rows) return null;
        return transform.GetChild(position.y * columns + position.x)?.GetComponent<Cell>();
    }

    public Cell Find(Cell cell) {
        return _cells.Find(c => cell == c);
    }

    public Vector2 GetGridSize() {
        return GetComponent<RectTransform>().rect.size;
    }

    public Vector2 GetCellSize() {
        return _layout.cellSize;
    }


    public void Clear()
    {
        int count = transform.childCount;
        while (count > 0)
        {
            Destroy(transform.GetChild(count - 1).gameObject);
            count--;
        }
        // List<Cell> cells = new();
        // for (int i = 0; i < count; i++)
        // {
        //     cells.Add(transform.GetChild(i).GetComponent<Cell>());
        // }
    }

    public void Setup()
    {
        if (_layout == null)
        {
            _layout = GetComponent<GridLayoutGroup>();
        }
        var gridSize = GetGridSize();

        float totalSpacingX = (columns - 1) * _layout.spacing.x;
        float totalSpacingY = (rows - 1) * _layout.spacing.y;

        float cellX = (gridSize.x - totalSpacingX) / columns;
        float cellY = (gridSize.y - totalSpacingY) / rows;

        if (cellX < cellY)
        {
            cellSize = cellX;
            float totalHeight = cellSize * rows + totalSpacingY;
            float offset = gridSize.y - totalHeight;
            if (offset > 0f)
            {
                _layout.padding.top = (int)(offset / 2);
                _layout.padding.bottom = (int)(offset / 2);

                _layout.padding.left = 0;
                _layout.padding.right = 0;
            }
        }
        else
        {
            cellSize = cellY;
            float totalWidth = cellSize * columns + totalSpacingX;
            float offset = gridSize.x - totalWidth;
            if (offset > 0f)
            {
                _layout.padding.left = (int)(offset / 2);
                _layout.padding.right = (int)(offset / 2);

                _layout.padding.top = 0;
                _layout.padding.bottom = 0;
            }
        }

        _layout.cellSize = new Vector2(cellSize, cellSize);
        _cells = GetCells();

        // for (int y = 0; y < rows; ++y)
        // {
        //     for (int x = 0; x < columns; ++x)
        //     {
        //         _cells[y * columns + x].position = new Vector2Int(x, y);
        //     }
        // }
    }

    
}
