using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        // �ڶ�������ʱִ�еĲ���
        Debug.Log("Animation ended, do something here!");
        GetComponent<Animator>().speed = 1;
    }
}
