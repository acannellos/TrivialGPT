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

    private AudioSource audioSource;
    [SerializeField] private AudioClip menuSound;
    [SerializeField] private AudioClip rollSound;
    
    private string[] welcomeMessages = new string[]
    {
        "Welcome to TrivialGPT!",
        "Answer a question in each Category to win.",
        " ",
        "Move with Left and Right Arrow keys.",
        "Press [R] or [Space] to roll.",
    };

    private void Awake()
    {
        data.ResetData();
        player.SetPlayerPosition(data.currentIndex);

        foreach (string message in welcomeMessages)
        {
            log.AddLog(message);
        }

        pauseMenu.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(menuSound);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    
    private void Update()
    {
        if (!log.isLogging)
        {
            if (data.stepsRemaining > 0)
            {
                player.HandleMovement();
            }
            else
            {
                HandleRoll();
            }
        }

        if (data.gameTurn > 0 && data.state != GameState.Victory) data.gameTime += Time.deltaTime;
        PauseControl();
    }

    private void HandleRoll()
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
                data.previousState = GameState.Rolling;

                audioSource.PlayOneShot(rollSound);
            }
        }
    }

    private void PauseControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (data.state == GameState.Paused)
            {
                data.state = data.previousState;
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                data.previousState = data.state;
                data.state = GameState.Paused;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        if (data.state == GameState.Paused && Input.GetKeyDown(KeyCode.Q))
        {
            QuitApplication();
        }
    }

    private void QuitApplication()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
