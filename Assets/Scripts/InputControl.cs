using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputControl : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject caret;
    [SerializeField] private LogControl log;
    [SerializeField] private GameData data;

    private void Start()
    {
        caret.SetActive(false);
        inputField.DeactivateInputField();
        inputField.text = string.Empty;
    }

    private void Update()
    {
        if (data.state == GameState.ReadyForInput)
        {
            inputField.enabled = true;
            caret.SetActive(true);
            inputField.ActivateInputField();
        }
        else
        {
            caret.SetActive(false);
            inputField.DeactivateInputField();
            inputField.text = string.Empty;
        }
    }

    public void ReadStringInput(string s)
    {
        if (inputField.text != string.Empty)
        {
            log.AddLog(s);
            data.state = GameState.Rolling;
            log.AddLog("ANSWER");
            log.AddLog("Ready to roll...");
        }
    }
}
