using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimator : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // ��ȡAnimator���
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ��Update�����и�������������ͬ�Ķ���
        if (Input.GetKeyDown(KeyCode.Space)) // ���磬���¿ո��ʱ������Ծ����
        {
            animator.SetTrigger("licking");
        }
    }
}
