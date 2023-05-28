using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Rolling,
    Moving,
    ReadyForInput
}

[CreateAssetMenu(menuName = "Game Data", fileName = "data")]
public class GameData : ScriptableObject
{
    public GameState state;

    public int currentIndex;
    public int rollResult;
    public int stepsRemaining;

    public string currentCategory;

    public float gameTime;
    public int gameTurn;

    public void RollDie()
    {
        rollResult = Random.Range(1, 7);
        stepsRemaining += rollResult;
        gameTurn++;
    }

    public void ResetData()
    {
        currentCategory = string.Empty;
        currentIndex = 3;
        rollResult = 0;
        stepsRemaining = 0;
        gameTime = 0;
        gameTurn = 0;
    }

    Dictionary<int, string> categories = new Dictionary<int, string>
    {
        { 0, "Geography" },
        { 1, "Entertainment" },
        { 2, "History" },
        { 3, "Start" },
        { 4, "Arts & Literature" },
        { 5, "Science & Nature" },
        { 6, "Sports & Leisure" }
    };

    public string GetStringForValue(int value)
    {
        if (categories.TryGetValue(value, out string stringValue))
        {
            return stringValue;
        }

        return string.Empty;
    }
}
