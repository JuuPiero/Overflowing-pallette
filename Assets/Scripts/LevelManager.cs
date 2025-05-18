using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    // public GameObject levelsContainer;
    public List<LevelDataSO> levels;
    public int current = 0;

    public static LevelManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    public void NextLevel()
    {
        current++;
        if (current >= levels.Count)
        {
            current = 0;
        }
    }

    public LevelDataSO GetCurrentLevel() => levels[current];

}