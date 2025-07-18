using System.Collections.Generic;
public class LevelManager : Singleton<LevelManager>
{
    public List<LevelDataSO> levels;
    public int current = 0;

    public void NextLevel()
    {
        current++;
        if (current >= levels.Count)
        {
            current = 0;
        }
    }

    public LevelDataSO GetCurrentLevelSO() => levels[current];
}