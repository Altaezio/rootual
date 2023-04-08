using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritSounds : MonoBehaviour
{
    [SerializeField] private List<Image> images = new List<Image>();
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] private AudioSource audioSource;
    private int memoryIndex;

    void Start()
    {
        memoryIndex = audioClips.Count;
    }

    public void ButtonClicked(int buttonIndex)
    {
        PlaySound(buttonIndex);
    }

    private void PlaySound(int buttonIndex)
    {
        if (memoryIndex < audioClips.Count)
        {
            StopSound(memoryIndex);
        }
        
        if(memoryIndex != buttonIndex)
        {
            audioSource.PlayOneShot(audioClips[buttonIndex]);
            images[buttonIndex].color = new Color(1.0f, 117/255f, 0.0f, 1.0f);

            memoryIndex = buttonIndex;
            StartCoroutine(ResetButtonColor(buttonIndex, audioClips[buttonIndex].length));
        } else {
            memoryIndex = audioClips.Count;
        }
    }

    private void StopSound(int buttonIndex)
    {
        audioSource.Stop();
        images[buttonIndex].color = Color.white;
    }

    private IEnumerator ResetButtonColor(int buttonIndex, float delay)
    {
        yield return new WaitForSeconds(4.0f);
        images[buttonIndex].color = Color.white;
    }
}
