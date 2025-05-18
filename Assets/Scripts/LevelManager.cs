using UnityEngine;
public class LevelManager : MonoBehaviour
{
    public LevelDataSO[] levels;
    // public Level currentLevel;
    public int current = 0;


    public static LevelManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    public void NextLevel()
    {
        current++;
        if (current >= levels.Length)
        {
            current = 0;
        }
    }

    public LevelDataSO GetCurrentLevel() => levels[current];
}