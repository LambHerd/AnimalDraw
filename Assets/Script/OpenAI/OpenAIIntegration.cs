using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

public class OpenAIIntegration : MonoBehaviour
{
    private string api_key = "sk-UBy8u92t9L4vVG39w2P1vUZw4Colo8ZLUejlwtYUO53sFDG2";
    private string base_url = "https://api.chatanywhere.tech/v1";

    public void CallOpenAIAPISync(string messages)
    {
        string url = base_url + "/chat/completions";
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "POST";
        request.Headers.Add("Authorization", "Bearer " + api_key);
        request.ContentType = "application/json";

        using (var streamWriter = new StreamWriter(request.GetRequestStream()))
        {
            string json = "{\"model\": \"gpt-3.5-turbo\",\"messages\": [{\"role\": \"user\",\"content\": \"" + messages + "\"}],\"stream\": false}";
            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        using (var streamReader = new StreamReader(response.GetResponseStream()))
        {
            string result = streamReader.ReadToEnd();
            Debug.Log(result);
            // 这里可以处理API的返回结果
        }
    }
}
