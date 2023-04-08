using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootAnimationManager : MonoBehaviour
{
    [SerializeField] private List<Animator> animators = new List<Animator>();
    [SerializeField] private List<AnimationClip> animationClips = new List<AnimationClip>();

    void Start()
    {
        for (int i = 0; i < animators.Count; i++)
        {
            animators[i].Play(animationClips[i].name);
        }

        InvokeRepeating(nameof(PlayAnimations), 0, animationClips[0].length);
    }

    private void PlayAnimations()
    {   
        this.gameObject.SetActive(false);
    }
}
