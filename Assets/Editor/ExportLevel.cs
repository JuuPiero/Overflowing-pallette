using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(GameManager))]
public class ExportLevel : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameManager myComp = (GameManager)target;
        if (GUILayout.Button("Export level"))
        {
            GridManager grid = FindAnyObjectByType<GridManager>();

            Level level = new Level();
            level.rows = grid.rows;
            level.columns = grid.columns;
            level.maxChange = myComp.changeCount;
            level.colors = myComp.colors.ToHexArray();
            level.target =  myComp.colors.IndexOf(myComp.target);

            int count = grid.transform.childCount;
            level.data = new int[count];

            for (int i = 0; i < count; i++)
            {
                Cell cell = grid.transform.GetChild(i).gameObject.GetComponent<Cell>();
                if (cell.CanChange)
                {
                    level.data[i] = myComp.colors.IndexOf(cell.color);
                }
                else
                {
                    level.data[i] = -1;
                }
            }

            string json = JsonUtility.ToJson(level, true);

            File.WriteAllText(Application.dataPath + "/Scripts/level.json", json);
        }
    }
}