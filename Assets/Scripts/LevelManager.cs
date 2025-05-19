using System.Collections.Generic;
using UnityEngine;
public class LevelManager : MonoBehaviour
{
    public List<LevelDataSO> levels;
    public int current = 0;

    public static LevelManager Instance { get; private set; }
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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