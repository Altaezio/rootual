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
    [SerializeField]
    private AudioSource audioSource;

    private float currentMoveSpeed;
    private Vector2 move;
    private float rotation;
    private bool fadingIn;

    private void Start()
    {
        currentMoveSpeed = DefaultMoveSpeed;
        fadingIn = false;
    }

    private void FixedUpdate()
    {
        rb.position += rb.rotation * (currentMoveSpeed * new Vector3(move.x, 0, move.y));
        rb.MoveRotation(Quaternion.Euler((rb.rotation.eulerAngles.y + rotation * rotationSpeed) * Vector3.up));
    }

    public void Move(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
        if (audioSource != null)
        {
            if (move == Vector2.zero)
            {
                fadingIn = false;
                StopCoroutine(nameof(FadeOutSound.StartFade));
                StartCoroutine(FadeOutSound.StartFade(audioSource, 1, 0));
            }
            else if (!fadingIn)
            {
                fadingIn = true;
                StopCoroutine(nameof(FadeOutSound.StartFade));
                StartCoroutine(FadeOutSound.StartFade(audioSource, .1f, 1));
            }
        }
    }

    public void Look(InputAction.CallbackContext context)
    {
        rotation = context.ReadValue<Vector2>().x;
    }

    public void UpdateMoveSpeed(float newSpeed)
    {
        currentMoveSpeed = newSpeed;
    }
}
