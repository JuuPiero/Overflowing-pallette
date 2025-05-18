using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;
using UnityEngine.UI;
public class GridEditorWindow : EditorWindow 
{
    private int rows = 5;
    private int columns = 5;
    private GameObject tilePrefab;

    private GameObject gridPrefab;


    [MenuItem("Tools/Grid Editor")]
    public static void ShowWindow()
    {
        GetWindow<GridEditorWindow>("Grid Editor");
    }

    private void OnGUI() {
        GUILayout.Label("Grid Settings", EditorStyles.boldLabel);

        rows = EditorGUILayout.IntField("Rows", rows);
        columns = EditorGUILayout.IntField("Columns", columns);
        tilePrefab = (GameObject)EditorGUILayout.ObjectField("Tile Prefab", tilePrefab, typeof(GameObject), false);
        gridPrefab = (GameObject)EditorGUILayout.ObjectField("Grid Prefab", gridPrefab, typeof(GameObject), false);
        if (GUILayout.Button("Generate Grid"))
        {
            GenerateGrid();
        }
    }

    private void GenerateGrid()
    {
        GameObject gridParent = (GameObject)PrefabUtility.InstantiatePrefab(gridPrefab);
        Canvas canvas = GameObject.FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            gridParent.transform.SetParent(canvas.transform, false);
        }
        gridParent.GetComponent<GridManager>().rows = rows;
        gridParent.GetComponent<GridManager>().columns = columns;
        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < columns; x++)
            {
                GameObject cell = (GameObject)PrefabUtility.InstantiatePrefab(tilePrefab);
                cell.transform.SetParent(gridParent.transform, false);
                cell.name = $"Cell_{x}_{y}";
            }
        }
        gridParent.GetComponent<GridManager>().Setup();
    }
}