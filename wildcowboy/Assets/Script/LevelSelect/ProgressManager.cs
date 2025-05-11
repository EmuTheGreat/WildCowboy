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
        // Уровень 0 всегда разблокирован
        if (levelNumber == 0) return true;

        return PlayerPrefs.GetInt(LevelKeyPrefix + levelNumber, 0) == 1;
    }
}
