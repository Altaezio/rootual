using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float DefaultMoveSpeed;
    public float MinMoveSpeed;

    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float rotationSpeed;

    private float currentMoveSpeed;
    private Vector2 move;
    private float rotation;
    private bool isRunning;
    private float runningRate = 1.5f;
    public bool immobilized = false;

    private void Start()
    {
        isRunning = false;
        currentMoveSpeed = DefaultMoveSpeed;
    }

    private void FixedUpdate()
    {
        if(!immobilized) // permet d'immobiliser les deux joueurs en cas d'attaque (à tester car j'ai pas de manette pour le moment)
        {
            rb.position += rb.rotation * (currentMoveSpeed * new Vector3(move.x, 0, move.y));
            rb.MoveRotation(Quaternion.Euler((rb.rotation.eulerAngles.y + rotation * rotationSpeed) * Vector3.up));
        }   
    }

    public void Move(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<Vector2>().x;
    }

    public void UpdateMoveSpeed(float newSpeed)
    {
        currentMoveSpeed = newSpeed;
    }

    public void Running(InputAction.CallbackContext context)
    {
        if (!isRunning && context.performed)
        {
            currentMoveSpeed *= runningRate;
            isRunning = true;
        }

        if (context.canceled && isRunning)
        {
            currentMoveSpeed /= runningRate;
            isRunning = false;
        }
    }

    public void IsImmobilized(bool immobilized) { this.immobilized = immobilized; }
}
