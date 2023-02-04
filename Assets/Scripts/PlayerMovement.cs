using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float DefaultMoveSpeed;
    
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float rotationSpeed;

    private float currentMoveSpeed;
    private Vector2 move;
    private float rotation;

    private void Start()
    {
        currentMoveSpeed = DefaultMoveSpeed;
    }

    private void FixedUpdate()
    {
        rb.position += rb.rotation * (currentMoveSpeed * new Vector3(move.x, 0, move.y));
        rb.MoveRotation(Quaternion.Euler((rb.rotation.eulerAngles.y + rotation * rotationSpeed) * Vector3.up));
    }

    public void Move(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<Vector2>().x;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("FEUUUU");
    }

    public void UpdateMoveSpeed(float newSpeed)
    {
        currentMoveSpeed = newSpeed;
    }
}
