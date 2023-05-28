using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameData data;
    [SerializeField] private LogControl log;
    [SerializeField] private Board board;
    private float lerpTime = 0.1f;

    private void Start()
    {
        SetPlayerPosition(data.currentIndex);
        data.state = GameState.Rolling;
    }

    private void Update()
    {
        if (data.stepsRemaining > 0)
        {
            HandleMovement();
        }
        else
        {
            if (data.state == GameState.Rolling)
            {
                if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space))
                {
                    data.RollDie();
                    if (data.rollResult < 6)
                    {
                        log.AddLog("You rolled a " + data.rollResult + ".");
                    }
                    else
                    {
                        log.AddLog("You rolled a " + data.rollResult + "!");
                    }
                    data.state = GameState.Moving;
                }
            }
        }
    }

    private void EndTurn()
    {
        data.state = GameState.Rolling;
    }

    private void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            data.currentIndex = (data.currentIndex - 1 + board.tileList.Count) % board.tileList.Count;
            SetPlayerPosition(data.currentIndex);
            data.currentCategory = data.GetStringForValue(data.currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            data.currentIndex = (data.currentIndex + 1) % board.tileList.Count;
            SetPlayerPosition(data.currentIndex);
            data.currentCategory = data.GetStringForValue(data.currentIndex);
        }
    }

    private void SetPlayerPosition(int index)
    {
        Vector3 targetPosition = board.tileList[index].position;
        StartCoroutine(MovePlayer(targetPosition));
    }

    private IEnumerator MovePlayer(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < lerpTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / lerpTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        if (data.stepsRemaining > 0)
        {
            data.stepsRemaining--;
        }

        if (data.stepsRemaining == 0 && data.gameTurn > 0)
        {
            if (data.currentCategory == "Start")
            {
                EndTurn();
                log.AddLog("Roll again...");
            }
            else
            {
                log.AddLog("You landed on " + data.currentCategory + ".");
                EndTurn();
                log.AddLog("QUESTION");
                log.AddLog("INPUT");
                log.AddLog("ANSWER");
                log.AddLog("Ready to roll...");
            }
        }
    }
}
