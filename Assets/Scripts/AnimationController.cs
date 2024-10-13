using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] float speed;
    public GameObject gameObject;

    public bool roll;


    void Start()
    {
        

    }


    void Update()
    {
        //speedChange();
        Rolling();
    }

   
    private void speedChange()
    {
        InputController player = gameObject.GetComponent<InputController>();
        speed = player.currentSpeed;
        animator.SetFloat("MoveSpeed", speed);
    }
    private void Rolling()
    {
        InputController player = gameObject.GetComponent<InputController>();
        roll = player.rolling;
        if (roll)
        {
            animator.SetTrigger("Roll");

        }
        else
        {
            animator.ResetTrigger("Roll");
        }
    }

}
