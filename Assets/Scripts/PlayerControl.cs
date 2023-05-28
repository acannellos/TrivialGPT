using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameData data;
    [SerializeField] private LogControl log;
    [SerializeField] private Board board;
    private float lerpTime = 0.1f;

    public void HandleMovement()
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

    public void SetPlayerPosition(int index)
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
            if (data.currentCategory == "Roll Again")
            {
                data.state = GameState.Rolling;
                log.AddLog("Roll again...");
            }
            else
            {
                data.state = GameState.ReadyForInput;
                log.AddLog("You landed on " + data.currentCategory + ".");
                log.AddLog("QUESTION");
            }
        }
    }
}
