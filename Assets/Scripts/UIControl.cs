using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIControl : MonoBehaviour
{
    [SerializeField] private GameData data;
    [SerializeField] private TextMeshProUGUI rollText;
    [SerializeField] private TextMeshProUGUI stepText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI turnText;

    void Update()
    {
        rollText.text = data.rollResult.ToString();

        stepText.text = data.stepsRemaining.ToString();
        
        float minutes = Mathf.FloorToInt(data.gameTime / 60);
        float seconds = Mathf.FloorToInt(data.gameTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        turnText.text = data.gameTurn.ToString();
    }


}
