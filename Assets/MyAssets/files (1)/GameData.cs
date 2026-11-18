using System;
using System.Collections.Generic;
using UnityEngine;

// Static save data: gems, unlocks, selections, power-ups, high scores.
// Replaces the earlier GameData (old Map scripts still work).
public static class GameData
{
    public static event Action OnGemsChanged;

    const int DefaultGems = 500;   // starting gems for testing (use 0 if your lab requires it)

    // ---------------- Gems ----------------
    public static int Gems { get { return PlayerPrefs.GetInt("Gems", DefaultGems); } }

    public static void AddGems(int amount)
    {
        PlayerPrefs.SetInt("Gems", Gems + amount);
        PlayerPrefs.Save();
        OnGemsChanged?.Invoke();
    }

    public static bool SpendGems(int amount)
    {
        if (Gems < amount) return false;
        PlayerPrefs.SetInt("Gems", Gems - amount);
        PlayerPrefs.Save();
        OnGemsChanged?.Invoke();
        return true;
    }

    // ------- Unlocks + selection (category = "Map", "Ball", "Track") -------
    public static bool IsUnlocked(string category, string id, bool freeByDefault = false)
    {
        return freeByDefault || PlayerPrefs.GetInt("Unlocked_" + category + "_" + id, 0) == 1;
    }

    public static void Unlock(string category, string id)
    {
        PlayerPrefs.SetInt("Unlocked_" + category + "_" + id, 1);
        PlayerPrefs.Save();
    }

    public static string GetSelected(string category, string defaultId)
    {
        return PlayerPrefs.GetString("Selected_" + category, defaultId);
    }

    public static void SetSelected(string category, string id)
    {
        PlayerPrefs.SetString("Selected_" + category, id);
        PlayerPrefs.Save();
    }

    // ------- Backward compatibility with the Map scripts -------
    public static bool IsUnlocked(string mapId) { return IsUnlocked("Map", mapId, mapId == "Original"); }
    public static void Unlock(string mapId) { Unlock("Map", mapId); }
    public static string SelectedMap
    {
        get { return GetSelected("Map", "Original"); }
        set { SetSelected("Map", value); }
    }

    // ---------------- Power-ups ----------------
    public static int GetPowerUps(string id) { return PlayerPrefs.GetInt("PowerUp_" + id, 0); }

    public static void AddPowerUp(string id, int amount)
    {
        PlayerPrefs.SetInt("PowerUp_" + id, GetPowerUps(id) + amount);
        PlayerPrefs.Save();
    }

    // ---------------- High scores (top 5) ----------------
    public const int ScoreSlots = 5;

    public static int[] GetHighScores()
    {
        int[] scores = new int[ScoreSlots];
        for (int i = 0; i < ScoreSlots; i++) scores[i] = PlayerPrefs.GetInt("Score_" + i, 0);
        return scores;
    }

    public static void AddScore(int score)
    {
        List<int> list = new List<int>(GetHighScores());
        list.Add(score);
        list.Sort((a, b) => b.CompareTo(a));
        for (int i = 0; i < ScoreSlots; i++) PlayerPrefs.SetInt("Score_" + i, list[i]);
        PlayerPrefs.Save();
    }

    // ---------------- Reset ----------------
    public static void ResetAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        OnGemsChanged?.Invoke();
    }
}
