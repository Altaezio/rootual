using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MrPropreAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Running runningBehaviour;
    private Quaternion targetRotation;
    private string currentAnimBool;

    private void Start()
    {
        runningBehaviour.ChangedSpeedToRunning += RunAnim;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localRotation = Quaternion.Slerp(this.transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void MoveAnim(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        float angle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
        targetRotation = Quaternion.Euler(0f, angle, 0f);

        if (context.performed)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void RunAnim(bool isRunning)
    {
        animator.SetBool("Running", isRunning);
    }

    public void PickUpAnim(string animBool)
    {
        currentAnimBool = animBool;
        animator.SetBool(animBool, true);
    }

    public void StopPickUpAnim()
    {
        if(currentAnimBool == null) return;
        animator.SetBool(currentAnimBool, false);
    }

    public void DeathAnim() { animator.SetTrigger("Death"); }
}
