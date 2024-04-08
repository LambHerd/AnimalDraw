using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEngine.EventSystems;
using UnityEditor;

public class ImageLoader : MonoBehaviour
{
    public Image imageDisplay;

    public Button change_btn;
    public Button upload_btn;

    byte[] fileData;
    void Start()
    {
        change_btn.onClick.AddListener(OnChangeButtonClick);
        upload_btn.onClick.AddListener(OnUploadButtonClick);
    }

    void OnChangeButtonClick()
    {
        SelectImage();
    }

    void SelectImage()
    {
        string imagePath = EditorUtility.OpenFilePanel("Select Image", "", "png,jpg,jpeg");

        if (imagePath.Length != 0)
        {
            fileData = File.ReadAllBytes(imagePath);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);

            if (imageDisplay != null)
            {
                imageDisplay.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
            }
        }
    }
    void OnUploadButtonClick()
    {
        if (fileData != null)
        {
            GetComponent<MessageQueue>().EnqueueMessage_img(fileData);
        }
        
    }
}