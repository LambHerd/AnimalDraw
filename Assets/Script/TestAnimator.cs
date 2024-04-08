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
        // 获取Animator组件
        animator = GetComponent<Animator>();
        animController = animator.runtimeAnimatorController as AnimatorController;
    }

    void Update()
    {
        // 在Update方法中根据条件触发不同的动画
        if (Input.GetKeyDown(KeyCode.Space)) // 例如，按下空格键时播放跳跃动画
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
