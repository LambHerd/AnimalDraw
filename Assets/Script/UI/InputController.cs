using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class InputController : MonoBehaviour
{
    public TMP_InputField inputFieldTMP;
    public Button input_btn;

    public List<GameObject> animals=new List<GameObject>();
    public List<string> motionTriggers = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        input_btn.onClick.AddListener(OnInputButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnInputButtonClick()
    {
        string inputText = inputFieldTMP.text;
        print("inputText: " + inputText);

        string msg_object = "";
        for(int i = 0; i < animals.Count; i++)
        {
            msg_object += animals[i].GetComponent<AnimalController>().animal_name;
            if (i != animals.Count - 1)
            {
                msg_object += ",";
            }

        }
        string msg_motion = "";
        for(int i = 0; i < motionTriggers.Count; i++)
        {
            msg_motion += motionTriggers[i];
            if (i != motionTriggers.Count - 1)
            {
                msg_motion += ",";
            }
        }

        IDictionary<string, string> messagedist = new Dictionary<string, string>();
        //messagedist.Add("message", "The lion combs his fur with his tongue");
        messagedist.Add("message", inputText);
        messagedist.Add("msg_object", msg_object);
        messagedist.Add("msg_motion", msg_motion);
        string str1 = ConvertDictionaryToJson(messagedist);
        print("ConvertDictionaryToJson: " + str1);

        GetComponent<MessageQueue>().EnqueueMessage(str1);
    }

    string ConvertDictionaryToJson(IDictionary<string, string> dictionary)
    {
        string json = "{";

        foreach (var kvp in dictionary)
        {
            json += "\"" + kvp.Key + "\":\"" + kvp.Value + "\",";
        }

        // Remove the trailing comma
        if (json.EndsWith(","))
        {
            json = json.Remove(json.Length - 1);
        }

        json += "}";

        return json;
    }

    public void getBackMsg(string object_value,string action_value)
    {
        for (int i = 0;i<animals.Count;i++)
        {
            if(object_value== animals[i].GetComponent<AnimalController>().animal_name)
            {
                animals[i].GetComponent<AnimalController>().setMotionTrigger(action_value);
                break;
            }
        }
    }
}
