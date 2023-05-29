using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldControl : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject caret;
    [SerializeField] private LogControl log;
    [SerializeField] private GameData data;
    [SerializeField] private TriviaControl trivia;

    private void Start()
    {
        DeactivateInputHandler();
    }

    private void Update()
    {
        if (data.state == GameState.ReadyForInput && !log.isLogging)
        {
            inputField.interactable = true;
            caret.SetActive(true);
            inputField.ActivateInputField();
        }
        else
        {
            DeactivateInputHandler();
        }
    }
    
    public void DeactivateInputHandler()
    {
        inputField.interactable = false;
        caret.SetActive(false);
        inputField.DeactivateInputField();
        inputField.text = string.Empty;
    }

    public void ReadStringInput(string s)
    {
        if (inputField.text != string.Empty)
        {
            log.AddLog(s);

            trivia.CheckQuestion(data.currentIndex, trivia.questionIndex, s);

            /*
            //TODO check if answer is correct
            if (s == "pineapple on pizza")
            {
                log.AddLog("Incorrect!");
                Debug.Log(data.categories[data.currentIndex].categoryName + " : " + data.categories[data.currentIndex].isAnswered);
            }
            else
            {
                log.AddLog("Correct!");
                data.categories[data.currentIndex].isAnswered = true;
                Debug.Log(data.categories[data.currentIndex].categoryName + " : " + data.categories[data.currentIndex].isAnswered);
                Debug.Log(data.CheckAllAnswered());
            }
            */
            log.AddLog("Ready to roll...");

            data.state = GameState.Rolling;
        }
    }
}
