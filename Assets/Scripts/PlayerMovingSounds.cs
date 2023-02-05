using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovingSounds : MonoBehaviour
{
    [SerializeField]
    private AudioSource walk;
    [SerializeField]
    private AudioSource heartBeat;
    [SerializeField]
    private float maxVolumeWalk;
    [SerializeField]
    private float maxVolumeHeart;
    [SerializeField]
    private float minVolumeWalk;
    [SerializeField]
    private float minVolumeHeart;
    [SerializeField]
    private float fadeInDurationWalk;
    [SerializeField]
    private float fadeInDurationHeart;
    [SerializeField]
    private float fadeOutDurationWalk;
    [SerializeField]
    private float fadeOutDurationHeart;

    public void IsMoving(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        if (move == Vector2.zero)
        {
            StopFadings();
            StartCoroutine(FadeOutSound.StartFade(walk, fadeOutDurationWalk, minVolumeWalk));
            StartCoroutine(FadeOutSound.StartFade(heartBeat, fadeInDurationHeart, maxVolumeHeart));
        }
        else
        {
            StopFadings();
            StartCoroutine(FadeOutSound.StartFade(walk, fadeInDurationWalk, maxVolumeWalk));
            StartCoroutine(FadeOutSound.StartFade(heartBeat, fadeOutDurationHeart, minVolumeHeart));
        }
    }

    private void StopFadings()
    {
        StopCoroutine(nameof(FadeOutSound.StartFade));
        StopCoroutine(nameof(FadeOutSound.StartFade));
    }
}
