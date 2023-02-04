using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodProperties : MonoBehaviour
{
    public float Weight;
    public float CollectTime;
    public GameObject loadBar;
    public Animator loadBarAnim;

    [SerializeField] private AudioSource audioSource;

    void Start()
    {

    }

    public void IsCollected()
    {
        audioSource.Play();
        loadBarAnim.speed = 1/CollectTime;
        loadBar.SetActive(true);
    }

    public void StoppedCollected()
    {
        audioSource.Stop();
        loadBar.SetActive(false);
    }
}
