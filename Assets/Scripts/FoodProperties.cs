using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodProperties : MonoBehaviour
{
    public float Weight;
    public float CollectTime;

    [SerializeField]
    private AudioSource audioSource;

    public void IsCollected()
    {
        audioSource.Play();
    }

    public void StoppedCollected()
    {
        audioSource.Stop();
    }
}
