using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor.Animations;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public string animal_name;

    private Animator animator;
    private AnimatorController animController;

    float aniSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        // 获取Animator组件
        animator = GetComponent<Animator>();
        animController = animator.runtimeAnimatorController as AnimatorController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMotionTrigger(string trigger)
    {
        print("set motion: "+trigger);
        animator.SetTrigger(trigger);
    }

    public void setMotionSpeed(string trigger,string speedstr)
    {
        if (speedstr == "notchange")
        {
            print("aniSpeed: " + "notchange");
            return;
        }
        else if (speedstr == "slow")
        {
            if (aniSpeed <= 1)
            {
                aniSpeed -= 0.2f;
            }
            else if (aniSpeed>1&&aniSpeed <= 3)
            {
                aniSpeed =1;
            }
            else if(aniSpeed>3)
            {
                aniSpeed -= 2;
            }

        }
        else if (speedstr == "fast")
        {
            aniSpeed += 2;
        }
        print("aniSpeed: " + aniSpeed);
        //SetAnimatorSpeed(0, trigger, aniSpeed);

        animator.speed = aniSpeed;
    }

    private void SetAnimatorSpeed(int _layer, string _stateName, float _speed)
    {
        for (int i = 0; i < animController.layers[_layer].stateMachine.states.Length; i++)
        {
            print(i+" _stateName: " + _stateName+"  "+ animController.layers[_layer].stateMachine.states[i].state.name);
            if (animController.layers[_layer].stateMachine.states[i].state.name == _stateName)
            {
                print("set speed: " + animController.layers[_layer].stateMachine.states[i].state.name + "  " + animController.layers[_layer].stateMachine.states[i].state.speed);
                animController.layers[_layer].stateMachine.states[i].state.speed = _speed;
            }
        }
    }

}
