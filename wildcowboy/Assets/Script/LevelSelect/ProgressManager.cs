using UnityEngine;

public static class ProgressManager
{
    private const string LevelKeyPrefix = "Level_";

    public static void UnlockLevel(int levelNumber)
    {
        PlayerPrefs.SetInt(LevelKeyPrefix + levelNumber, 1);
    }

    public static bool IsLevelUnlocked(int levelNumber)
    {
        return PlayerPrefs.GetInt(LevelKeyPrefix + levelNumber, levelNumber == 1 ? 1 : 0) == 1;
    }
}
