using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusMachine : StateMachineBehaviour
{
    // 进入状态调一次
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Enter state" );
    }

    // 在状态中每帧调用一次
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // 离开状态调用一次
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exit state");
        animator.speed = 1;
    }

    // 动画对象移动时(动画未设置烘焙位置Bake Info Pose), 每帧调用一次
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // 在Animator.OnAnimatorIK()之后调用, IK(逆向运动学)的实现可以写在这里(需要开启IK Pass)
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
