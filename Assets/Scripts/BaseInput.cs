using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseInput : MonoBehaviour
{
    [Header("Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool roll;
    public bool sprint;

    [Header("Movemnet Settings")]
    public bool analogMovement;


    // InputSystem È°¼º
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }
    public void OnRoll(InputValue value)
    {
        RollInput(value.isPressed);
    }
    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection; 
    }
    public void RollInput(bool newRollState)
    {
        roll = newRollState;
    }
    public void SprintInput(bool newSpirntState)
    {
        sprint = newSpirntState;
    }
}
