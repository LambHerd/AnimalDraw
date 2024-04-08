using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TestObjpath : MonoBehaviour
{
    public Button btn;
    
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(OnButtonClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnButtonClick()
    {
        string objFilePath = Path.Combine("E:\\project_unity\\AnimalDraw\\temp_obj", "model.obj");
        print(objFilePath);
        Object objFile = AssetDatabase.LoadAssetAtPath<Object>(objFilePath);
        print(objFile);
        GameObject obj = Instantiate(objFile) as GameObject;
        print(obj);
    }


}
