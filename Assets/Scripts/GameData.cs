using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Rolling,
    Moving,
    ReadyForInput,
    Paused
}

public class CategoryData
{
    public string categoryName;
    public Color categoryColor;
    public string categoryHex;
    public bool isAnswered;
}

[CreateAssetMenu(menuName = "Game Data", fileName = "data")]
public class GameData : ScriptableObject
{
    public GameState state;
    public GameState previousState;

    public int currentIndex;
    public int currentRoll;
    public int stepsRemaining;

    public string currentCategory;

    public float gameTime;
    public int gameTurn;

    public bool endGame;

    public void RollDie()
    {
        currentRoll = Random.Range(1, 7);
        stepsRemaining += currentRoll;
        gameTurn++;
    }

    public void ResetData()
    {
        state = GameState.Rolling;
        previousState = GameState.Rolling;
        currentCategory = string.Empty;
        currentIndex = 3;
        currentRoll = 0;
        stepsRemaining = 0;
        gameTime = 0;
        gameTurn = 0;
    }

    public Dictionary<int, CategoryData> categories = new Dictionary<int, CategoryData>
    {
        { 0, new CategoryData { categoryName = "Geography", categoryColor = Color.red, categoryHex = "#FF0000", isAnswered = false } },
        { 1, new CategoryData { categoryName = "Entertainment", categoryColor = Color.green, categoryHex = "#00FF00", isAnswered = false } },
        { 2, new CategoryData { categoryName = "History", categoryColor = Color.blue, categoryHex = "#0000FF", isAnswered = false } },
        { 3, new CategoryData { categoryName = "Roll Again", categoryColor = Color.yellow, categoryHex = "#FFFF00", isAnswered = false } },
        { 4, new CategoryData { categoryName = "Arts & Literature", categoryColor = Color.magenta, categoryHex = "#FF00FF", isAnswered = false } },
        { 5, new CategoryData { categoryName = "Science & Nature", categoryColor = Color.cyan, categoryHex = "#00FFFF", isAnswered = false } },
        { 6, new CategoryData { categoryName = "Sports & Leisure", categoryColor = Color.gray, categoryHex = "#808080", isAnswered = false } }
    };

    public string GetCategoryName(int value)
    {
        if (categories.TryGetValue(value, out CategoryData categoryData))
        {
            return categoryData.categoryName;
        }

        return string.Empty;
    }

    public bool GetCategoryAnswer(int value)
    {
        if (categories.TryGetValue(value, out CategoryData categoryData))
        {
            return categoryData.isAnswered;
        }

        return false;
    }

    public bool CheckAllAnswered()
    {
        foreach (KeyValuePair<int, CategoryData> category in categories)
        {
            if (!category.Value.isAnswered)
            {
                return false;
            }
        }
        return true;
    }
}
