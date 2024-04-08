using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Networking;

public class MessageQueue : MonoBehaviour
{
    string url = "http://127.0.0.1:5000/";

    Queue<MessageInQueue> messageQueue = new Queue<MessageInQueue>();

    class MessageInQueue
    {
        public int type;
        public string message_str;
        public byte[] message_img;
    }

    void Start()
    {
        StartCoroutine(ProcessQueue());
    }

    void Update()
    {

    }

    public void EnqueueMessage_str(string message)
    {
        MessageInQueue messageInQueue = new MessageInQueue();
        messageInQueue.type = 0;
        messageInQueue.message_str = message;
        messageQueue.Enqueue(messageInQueue);
    }
    public void EnqueueMessage_img(byte[] message)
    {
        MessageInQueue messageInQueue = new MessageInQueue();
        messageInQueue.type = 1;
        messageInQueue.message_img = message;
        messageQueue.Enqueue(messageInQueue);
    }

    IEnumerator ProcessQueue()
    {
        while (true)
        {
            if (messageQueue.Count > 0)
            {
                MessageInQueue messageInQueue = messageQueue.Dequeue();
                if (messageInQueue.type == 0)
                {
                    StartCoroutine(sendMessage(messageInQueue.message_str));
                }
                else if (messageInQueue.type == 1)
                {
                    StartCoroutine(sendImage(messageInQueue.message_img));
                }
            }

            yield return null;
        }
    }

    [System.Serializable]
    public class MessageData
    {
        public string object_value;
        public string action_value;
        public string action_speed;
    }

    IEnumerator sendMessage(string message)
    {
        //print("message: " + message);

        //string jsonData = "{\"message\": \"" + message + "\"}";

        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(message);

        using (UnityWebRequest request = new UnityWebRequest(url+"unity_msg", "POST"))
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

                Debug.Log("Received object_value: " + receivedMessage.object_value);
                Debug.Log("Received action_value: " + receivedMessage.action_value);
                Debug.Log("Received action_speed: " + receivedMessage.action_speed);

                GetComponent<InputController>().getBackMsg(receivedMessage.object_value, receivedMessage.action_value, receivedMessage.action_speed);
            }
        }
    }

    int COUNT = 0;

    IEnumerator sendImage(byte[] imageBytes)
    {
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", imageBytes, "image.png", "image/png"); // 设置文件字段的名称和类型

        UnityWebRequest www = UnityWebRequest.Post(url + "unity_img", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Image upload successful");
            string receivedJson = www.downloadHandler.text;
            Debug.Log("Received msg: " + receivedJson);


            www = UnityWebRequest.Get(url + "unity_obj");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                byte[] objBytes = www.downloadHandler.data;
                print("recive obj");
                www = UnityWebRequest.Get(url + "unity_mtl");
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    byte[] pngBytes = www.downloadHandler.data;
                    print("recive mtl");

                    string folderPath = Path.Combine("Assets", "Model","To3d", COUNT.ToString());
                    // 检查文件夹是否存在，如果不存在则创建
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                        Debug.Log("Folder created at: " + folderPath);
                    }
                    else
                    {
                        Debug.Log("Folder already exists at: " + folderPath);
                    }


                    // 保存OBJ文件 E:\project_unity\AnimalDraw\temp_obj Application.dataPath
                    //string objFilePath = Path.Combine(Application.persistentDataPath, "model.obj");
                    //string objFilePath = Path.Combine("E:\\project_unity\\AnimalDraw\\temp_obj", "model.obj");
                    string objFilePath = Path.Combine(folderPath, "model.obj");
                    File.WriteAllBytes(objFilePath, objBytes);

                    // 保存PNG文件
                    //string pngFilePath = Path.Combine(Application.persistentDataPath, "texture.png");
                    string pngFilePath = Path.Combine(folderPath, "texture.png");
                    File.WriteAllBytes(pngFilePath, pngBytes);

                    // 在Unity编辑器中刷新资源
                    UnityEditor.AssetDatabase.Refresh();

                    // 加载.obj文件作为模型
                    GameObject model = LoadOBJ(objFilePath);

                    if (model != null)
                    {
                        // 加载.png文件作为材质
                        Material material = LoadPNG(pngFilePath);

                        if (material != null)
                        {
                            // 将材质赋予模型
                            GameObject g = model.transform.GetChild(0).gameObject;
                            g.GetComponent<Renderer>().material = material;

                            // 实例化模型在游戏中
                            Instantiate(g, Vector3.zero, Quaternion.identity);
                        }
                    }

                    COUNT++;

                    Debug.Log("Files downloaded and loaded successfully");
                }
            }
        }
    }

    GameObject LoadOBJ(string filePath)
    {
        // 使用AssetDatabase导入.obj文件
        Object objFile = AssetDatabase.LoadAssetAtPath<Object>(filePath);
        print(objFile+" : "+ filePath);
        GameObject obj = Instantiate(objFile) as GameObject;
        print(obj);
        return obj;
    }

    Material LoadPNG(string filePath)
    {
        Material material = new Material(Shader.Find("Standard"));

        if (File.Exists(filePath))
        {
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(File.ReadAllBytes(filePath));
            material.mainTexture = texture;
        }
        else
        {
            Debug.LogError("PNG file not found: " + filePath);
            material = null;
        }

        return material;
    }
}