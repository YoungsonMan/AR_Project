using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] PlayerInput input;

    [SerializeField] Rigidbody rigid;

    [SerializeField] public float movePower;
    [SerializeField] public float jumpPower;

    public float currentSpeed;
    public bool rolling;

    public bool isMoving;

    private void Update()
    {
        // Move
        Vector2 move = input.actions["Move"].ReadValue<Vector2>();
        Vector3 dir = new Vector3(move.x, 0, move.y);
        rigid.velocity = dir * movePower + Vector3.up * rigid.velocity.y;
        currentSpeed = move.magnitude;

        

        bool roll = input.actions["Roll"].WasPressedThisFrame();
        if (roll)
        {
            Debug.Log($"±¸¸£±â! {currentSpeed}");
            rigid.AddForce(dir * jumpPower , ForceMode.Impulse);
            rolling = roll;
        }
        else
        {
            rolling = false;
        }
    }
    private void FixedUpdate()
    {
        
    }

    public void Roll()
    {
        Vector2 move = input.actions["Move"].ReadValue<Vector2>();
        Vector3 dir = new Vector3(move.x, 0, move.y);
        rigid.AddForce(dir * jumpPower, ForceMode.Impulse);
    }
}
