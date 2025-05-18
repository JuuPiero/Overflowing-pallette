using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewLevel", menuName = "Data/New Level Data")]
public class LevelDataSO : ScriptableObject
{
    public TextAsset jsonFile;
    public bool canPlay = true;
    

    public Level GetLevel()
    {
        return JsonUtility.FromJson<Level>(jsonFile.text);
    }
}
