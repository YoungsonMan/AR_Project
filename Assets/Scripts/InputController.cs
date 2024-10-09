using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] PlayerInput input;

    [SerializeField] Rigidbody rigid;

    [SerializeField] float movePower;
    [SerializeField] float jumpPower;

    private void Update()
    {
        // Move
        Vector2 move = input.actions["Move"].ReadValue<Vector2>();
        Vector3 dir = new Vector3(move.x, 0, move.y);
        rigid.velocity = dir * movePower + Vector3.up * rigid.velocity.y;



        bool roll = input.actions["Roll"].WasPressedThisFrame();
        if (roll)
        {
            Debug.Log("±¸¸£±â!");
            rigid.AddForce(dir * jumpPower , ForceMode.Impulse);
        }
    }

    public void Roll()
    {
        Vector2 move = input.actions["Move"].ReadValue<Vector2>();
        Vector3 dir = new Vector3(move.x, 0, move.y);
        rigid.AddForce(dir * jumpPower, ForceMode.Impulse);
    }
}
