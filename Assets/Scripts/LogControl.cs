using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogControl : MonoBehaviour
{
    [SerializeField] private GameObject textTemplate;
    [SerializeField] private float welcomeDelay = 1f;
    [SerializeField] private int maxLogTexts = 10;
    [SerializeField] private int maxLogChars = 50;
    [SerializeField] private float typingSpeed = 0.1f;

    private List<GameObject> logTexts = new List<GameObject>();

    private bool isTyping = false;

    private Queue<string> logQueue = new Queue<string>();

    public void AddLog(string text)
    {
        if (text.Length > maxLogChars)
        {
            int splitIndex = text.LastIndexOf(' ', maxLogChars);
            string firstLine = text.Substring(0, splitIndex);
            string secondLine = text.Substring(splitIndex + 1);
            logQueue.Enqueue(firstLine);
            AddLog(secondLine);
            return;
        }
        else
        {
            logQueue.Enqueue(text);
        }
        
        if (!isTyping)
        {
            StartCoroutine(TypeNextLog());
        }
    }

    private IEnumerator TypeNextLog()
    {
        if (logQueue.Count == 0)
        {
            yield break;
        }

        isTyping = true;

        string nextLog = logQueue.Dequeue();

        if (logTexts.Count >= maxLogTexts + 1)
        {
            Destroy(logTexts[0]);
            logTexts.RemoveAt(0);
        }

        GameObject newText = Instantiate(textTemplate) as GameObject;
        
        newText.SetActive(true);
        newText.transform.SetParent(textTemplate.transform.parent, false);

        TextMeshProUGUI textMesh = newText.GetComponent<TextMeshProUGUI>();
        yield return StartCoroutine(TypeText(textMesh, nextLog));
        
        logTexts.Add(newText);

        isTyping = false;
        StartCoroutine(TypeNextLog());
    }

    private IEnumerator TypeText(TextMeshProUGUI textMesh, string text)
    {
        isTyping = true;
        textMesh.text = ""; // Clear the text initially
        foreach (char c in text)
        {
            textMesh.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private string[] welcomeMessages = new string[]
    {
        "Welcome to TrivialGPT!",
        "Answer a question in each Category, then",
        "return to the center for a final question. lorum ipsum this is a major test again to see what happens when we load in a really long string hopefully this is long enough to see what happens!! cooool eao . bye",
        " ",
        "Move with Left and Right Arrow keys.",
        "Press [R] or [Space] to roll.",
        "Ready to roll..."
    };

    private void Start()
    {
        foreach (string message in welcomeMessages)
        {
            AddLog(message);
        }
    }
}
