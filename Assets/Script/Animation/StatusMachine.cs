using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusMachine : StateMachineBehaviour
{
    // ����״̬��һ��
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Enter state" );
    }

    // ��״̬��ÿ֡����һ��
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // �뿪״̬����һ��
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exit state");
        animator.speed = 1;
    }

    // ���������ƶ�ʱ(����δ���ú決λ��Bake Info Pose), ÿ֡����һ��
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    // ��Animator.OnAnimatorIK()֮�����, IK(�����˶�ѧ)��ʵ�ֿ���д������(��Ҫ����IK Pass)
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
