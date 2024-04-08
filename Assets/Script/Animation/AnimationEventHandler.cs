using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        // 在动画结束时执行的操作
        Debug.Log("Animation ended, do something here!");
        GetComponent<Animator>().speed = 1;
    }
}
