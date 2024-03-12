using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    public string animal_name;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // ��ȡAnimator���
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMotionTrigger(string trigger)
    {
        animator.SetTrigger(trigger);
    }
}
