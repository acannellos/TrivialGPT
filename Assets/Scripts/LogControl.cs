using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogControl : MonoBehaviour
{
    [SerializeField] private GameData data;
    [SerializeField] private GameObject textTemplate;
    [SerializeField] private int maxLogTexts = 10;
    [SerializeField] private int maxLogChars = 50;
    [SerializeField] private float typingSpeed = 0.1f;

    private List<GameObject> logTexts = new List<GameObject>();

    public bool isLogging = false;

    private Queue<string> logQueue = new Queue<string>();

    public void AddLog(string text)
    {
        if (text.Length > maxLogChars)
        {
            string firstLine = text.Substring(0, maxLogChars);
            string secondLine = text.Substring(maxLogChars + 1);
            logQueue.Enqueue(firstLine);
            AddLog(secondLine);
            return;
        }
        else
        {
            logQueue.Enqueue(text);
        }
        
        if (!isLogging)
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

        isLogging = true;

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

        isLogging = false;
        StartCoroutine(TypeNextLog());
    }

    private IEnumerator TypeText(TextMeshProUGUI textMesh, string text)
    {
        isLogging = true;
        textMesh.text = "";
        foreach (char c in text)
        {
            textMesh.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isLogging = false;
    }
}
