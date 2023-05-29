using UnityEngine;
using TMPro;

public class UIControl : MonoBehaviour
{
    [SerializeField] private GameData data;
    [SerializeField] private TextMeshProUGUI rollText;
    [SerializeField] private TextMeshProUGUI stepText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI turnText;

    [SerializeField] private Color answeredColor;
    [SerializeField] private GameObject textMeshContainer;
    private TextMeshProUGUI[] textMeshPros;

    private void Awake()
    {
        textMeshPros = textMeshContainer.GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        HandleDashboard();
        HandleCategories();
    }

    private void HandleDashboard()
    {
        rollText.text = data.currentRoll.ToString();
        stepText.text = data.stepsRemaining.ToString();

        float minutes = Mathf.FloorToInt(data.gameTime / 60);
        float seconds = Mathf.FloorToInt(data.gameTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        turnText.text = data.gameTurn.ToString();
    }

    private void HandleCategories()
    {
        for (int i = 0; i < textMeshPros.Length; i++)
        {
            if (data.GetCategoryAnswer(i))
            {
                textMeshPros[i].color = answeredColor;
            }
        }
    }
}
