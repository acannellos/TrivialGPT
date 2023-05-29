using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class OpenAIControl : MonoBehaviour
{
    [SerializeField] private LogControl log;

    private const string openAIEndpoint = "https://api.openai.com/v1/completions";

    // TODO store this value in a more secure location if this script is ever needed
    private const string apiKey = "YOUR_API_KEY";

    // The trivia category you want to use
    //private string triviaCategory = "History";

    public async void GenerateQuestion(string triviaCategory)
    {
        // Call the OpenAI API to generate a History trivia question
        string generatedQuestion = await RequestQuestion(triviaCategory);

        // Log the generated question
        Debug.Log("Generated Question: " + generatedQuestion);
        log.AddLog(generatedQuestion);
    }

    private async Task<string> RequestQuestion(string triviaCategory)
    {
        // Create the prompt to request a question in the desired category
        string prompt = $"Generate a {triviaCategory} trivia question";

        // Create the JSON payload string manually
        string jsonPayload = "{" +
            "\"model\": \"text-davinci-003\"," +
            "\"prompt\": \"" + prompt + "\"," +
            "\"max_tokens\": 10," +
            "\"temperature\": 0," +
            "\"top_p\": 1," +
            "\"n\": 1" +
            "}";
        Debug.Log("JSON Payload: " + jsonPayload);

        UnityWebRequest request = new UnityWebRequest(openAIEndpoint, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        var operation = request.SendWebRequest();

        while (!operation.isDone)
        {
            await Task.Delay(100);
        }

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            var responseObject = JsonUtility.FromJson<OpenAICompletionResponse>(response);
            return responseObject.choices[0].text.Trim();
        }
        else
        {
            Debug.LogError("Failed to generate the trivia question. Error: " + request.error);
            return null;
        }
    }
}

[Serializable]
public class OpenAICompletionResponse
{
    public OpenAICompletionChoice[] choices;
}

[Serializable]
public class OpenAICompletionChoice
{
    public string text;
}
