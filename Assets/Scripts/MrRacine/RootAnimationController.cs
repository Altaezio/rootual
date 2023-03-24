using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimationClip clip;

    private void Start()
    {
        animator.Play(clip.name);
    }

    public void DestroysSelf()
    {
        // Destroy(gameObject);
        this.gameObject.SetActive(false);
    }
}
