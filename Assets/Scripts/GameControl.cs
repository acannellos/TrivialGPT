using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameControl : MonoBehaviour
{
    [SerializeField] private GameData data;
    [SerializeField] private PlayerControl player;
    [SerializeField] private LogControl log;
    [SerializeField] private GameObject pauseMenu;
    
    public bool isPaused;

    private void Awake()
    {
        data.ResetData();
        player.SetPlayerPosition(data.currentIndex);
        data.state = GameState.Rolling;

        foreach (string message in welcomeMessages)
        {
            log.AddLog(message);
        }

        pauseMenu.SetActive(false);
        isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void Update()
    {
        if (data.stepsRemaining > 0)
        {
            player.HandleMovement();
        }
        else
        {
            if (data.state == GameState.Rolling)
            {
                if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space))
                {
                    data.RollDie();
                    if (data.currentRoll < 6)
                    {
                        log.AddLog("You rolled a " + data.currentRoll + ".");
                    }
                    else
                    {
                        log.AddLog("You rolled a " + data.currentRoll + "!");
                    }
                    data.state = GameState.Moving;
                }
            }
        }

        data.gameTime += Time.deltaTime;
        
        PauseControl();
    }

    private void PauseControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                pauseMenu.SetActive(false);
                isPaused = false;
                Time.timeScale = 1f;
            }
            else
            {
                pauseMenu.SetActive(true);
                isPaused = true;
                Time.timeScale = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && isPaused)
        {
            QuitApplication();
        }
    }

    void QuitApplication()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    private string[] welcomeMessages = new string[]
    {
        "Welcome to TrivialGPT!",
        "Answer a question in each Category, then",
        "return to the center for a final question.",
        " ",
        "Move with Left and Right Arrow keys.",
        "Press [R] or [Space] to roll.",
        "Ready to roll..."
    };
}
