using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimator : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // 获取Animator组件
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 在Update方法中根据条件触发不同的动画
        if (Input.GetKeyDown(KeyCode.Space)) // 例如，按下空格键时播放跳跃动画
        {
            animator.SetTrigger("licking");
        }
    }
}
