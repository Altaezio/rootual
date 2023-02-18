using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodProperties : MonoBehaviour
{
    public float Weight;
    public float CollectTime;
    public string FoodType;
    [SerializeField] private GameObject collectBar;
    [SerializeField] private Animator collectBarAnim;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float probabilityToAppear;
    [SerializeField] private Collider collectingArea;

    void Start()
    {
        GameObject parent = transform.root.gameObject;
        collectBar = parent.GetComponent<PerlinNoise>().CollectBar;
        collectBarAnim = parent.GetComponent<PerlinNoise>().CollectBarAnim;

        if (Random.Range(0.0f, 1.0f) > probabilityToAppear)
        {
            Destroy(this.gameObject);
        } else {
            collectingArea.enabled = true;
        }
    }

    public void IsCollected()
    {
        audioSource.Play();
        collectBarAnim.speed = 1/CollectTime;
        collectBar.SetActive(true);
    }

    public void StoppedCollected()
    {
        audioSource.Stop();
        collectBar.SetActive(false);
    }
}
