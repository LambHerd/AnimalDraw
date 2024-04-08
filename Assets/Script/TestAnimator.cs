using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TestAnimator : MonoBehaviour
{
    private Animator animator;
    private AnimatorController animController;
    public float aniSpeed = 2f;

    void Start()
    {
        // ��ȡAnimator���
        animator = GetComponent<Animator>();
        animController = animator.runtimeAnimatorController as AnimatorController;
    }

    void Update()
    {
        // ��Update�����и�������������ͬ�Ķ���
        if (Input.GetKeyDown(KeyCode.Space)) // ���磬���¿ո��ʱ������Ծ����
        {
            print(animController.layers[0].stateMachine.states[0].state.speed);
            animator.SetTrigger("licking");
            setMotionSpeed("licking");
        }
    }

    public void setMotionSpeed(string trigger)
    {
        SetAnimatorSpeed(0, trigger, aniSpeed);
    }

    private void SetAnimatorSpeed(int _layer, string _stateName, float _speed)
    {
        for (int i = 0; i < animController.layers[_layer].stateMachine.states.Length; i++)
        {
            if (animController.layers[_layer].stateMachine.states[i].state.name == _stateName)
            {
                animController.layers[_layer].stateMachine.states[i].state.speed = _speed;
            }
        }
    }
}
