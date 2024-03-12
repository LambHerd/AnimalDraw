using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MessageQueuePrototype : MonoBehaviour
{
    string url = "http://127.0.0.1:5000/api";

    Queue<string> messageQueue = new Queue<string>();

    void Start()
    {
        StartCoroutine(ProcessQueue());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EnqueueMessage("New message to backend");
        }
    }

    void EnqueueMessage(string message)
    {
        messageQueue.Enqueue(message);
    }

    IEnumerator ProcessQueue()
    {
        while (true)
        {
            if (messageQueue.Count > 0)
            {
                string message = messageQueue.Dequeue();
                StartCoroutine(SendMessage(message));
            }

            yield return null;
        }
    }

    [System.Serializable]
    public class MessageData
    {
        public string message;
    }

    IEnumerator SendMessage(string message)
    {
        print("message: " + message);

        string jsonData = "{\"message\": \"" + message + "\"}";

        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);
            }
            else
            {
                


                string receivedJson = request.downloadHandler.text;
                Debug.Log("Received json: " + receivedJson);
                MessageData receivedMessage = JsonUtility.FromJson<MessageData>(receivedJson);

                Debug.Log("Received message: " + receivedMessage.message);
            }
        }
    }
}