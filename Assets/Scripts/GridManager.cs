using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class GridManager : MonoBehaviour {


    public event Action OnFilled;

    [SerializeField] private GameObject _cellPrefab;

    public float cellSize;
    public int rows;
    public int columns;
    public Dictionary<Vector2Int, Cell> cells = new();

    public bool isLoading = true;    
    public bool isFilling = false;

    private GridLayoutGroup _layout;

  

    public IEnumerator Loading() {
        isLoading = true;
        yield return new WaitForSeconds(0.3f);
        Setup();
        isLoading = false;
    }

    public void Fill(int x, int y, Color colorTochange)
    {
        var targetCell = GetCell(x, y);
        if (isFilling || !targetCell.CanChange) return;
        if (colorTochange.Equals(targetCell.color)) return;
        GameManager.Instance?.ReduceChangeCount();
        OnFilled?.Invoke();
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
        // int count = transform.childCount;
        // List<Cell> cells = new();
        // for (int i = 0; i < count; i++)
        // {
        //     cells.Add(transform.GetChild(i).GetComponent<Cell>());
        // }
        return cells.Values.ToList<Cell>();
    }

    public Cell GetCell(int x, int y) {
        return GetCell(new Vector2Int(x, y));
    }
    public Cell GetCell(Vector2Int position)
    {
        return cells[position];
    }


    public Vector2 GetGridSize() {
        return GetComponent<RectTransform>().rect.size;
    }

    public Vector2 GetCellSize() {
        return _layout.cellSize;
    }


    public void Clear()
    {
        transform.ClearChild();
        cells.Clear();
    }


    public void Setup()
    {
        if (_layout == null)
        {
            _layout = GetComponent<GridLayoutGroup>();
        }

        // Calculate cell size and cell spacing
        Vector2 gridSize = GetGridSize();
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
    }
}
