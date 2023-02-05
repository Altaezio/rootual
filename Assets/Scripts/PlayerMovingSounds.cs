using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovingSounds : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip walk;
    [SerializeField]
    private AudioClip heartBeat;
    [SerializeField]
    private float maxVolume;
    [SerializeField]
    private float fadeDuration;

    [SerializeField]
    private float targetVolume;
    private bool isWalkPlaying;

    private void Start()
    {
        isWalkPlaying = false;
        StartCoroutine(PermanentFade());
    }

    public void IsMoving(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        if (audioSource != null)
        {
            if (move == Vector2.zero)
            {
                if (isWalkPlaying)
                    StopCoroutine(Swap());
                StartCoroutine(Swap());
            }
            else
            {
                if (!isWalkPlaying)
                    StopCoroutine(Swap());
                StartCoroutine(Swap());
            }
        }
    }

    private IEnumerator PermanentFade()
    {
        while (true)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime / fadeDuration);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private IEnumerator Swap()
    {
        targetVolume = 0;
        while (audioSource.volume > 0)
        {
            yield return new WaitForSeconds(.5f);
        }
        audioSource.clip = isWalkPlaying ? heartBeat : walk;
        targetVolume = maxVolume;
    }
}
