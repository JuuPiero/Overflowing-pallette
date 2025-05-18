using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridManager))]
public class ResetGrid : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GridManager myComp = (GridManager)target;
        if (GUILayout.Button("Reset"))
        {
            myComp.Setup();
        }
    }
}