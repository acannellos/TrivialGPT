using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private GameData data;
    [SerializeField] private LogControl log;
    [SerializeField] private Board board;
    private float lerpTime = 0.1f;

    //[SerializeField] private OpenAIControl openAI;
    [SerializeField] private TriviaControl trivia;

    public void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            data.currentIndex = (data.currentIndex - 1 + board.tileList.Count) % board.tileList.Count;
            SetPlayerPosition(data.currentIndex);
            data.currentCategory = data.GetCategoryName(data.currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            data.currentIndex = (data.currentIndex + 1) % board.tileList.Count;
            SetPlayerPosition(data.currentIndex);
            data.currentCategory = data.GetCategoryName(data.currentIndex);
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
                data.previousState = GameState.Moving;
                log.AddLog("Roll again...");
            }
            else
            {
                data.state = GameState.ReadyForInput;
                data.previousState = GameState.Moving;
                log.AddLog("You landed on " + data.currentCategory + ".");
                log.AddLog(trivia.GetRandomQuestion(data.currentIndex).question);

                //log.AddLog("What is the meaning of life?");
                //openAI.GenerateQuestion(data.currentCategory);
            }
        }
    }
}
