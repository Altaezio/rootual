using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MrPropreAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float rotationSpeed = 100f;
    private Quaternion targetRotation;
    private string currentAnimBool;

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

        if(context.performed){
            animator.SetBool("Walking", true);
        }else{
            animator.SetBool("Walking", false);
        }
    }

    public void RunAnim(InputAction.CallbackContext context)
    {
        if(context.performed){
            animator.SetBool("Running", true);
        }else{
            animator.SetBool("Running", false);
        }
    }

    public void PickUpAnim(string animBool)
    {   
        currentAnimBool = animBool;
        animator.SetBool(animBool, true); 
    }

    public void StopPickUpAnim()
    {
        animator.SetBool(currentAnimBool, false); 
    }
    
    public void DeathAnim(){ animator.SetTrigger("Death"); }
}
