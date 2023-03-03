using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbianceSound : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private ProceduralMap map;
    [SerializeField] private float beginDelay, maxDelayBetweenTwo, minHeight, maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAmbianceLoop());
    }

    private IEnumerator StartAmbianceLoop()
    {
        yield return new WaitForSeconds(beginDelay);
        StartCoroutine(AmbianceLoop());
    }

    private IEnumerator AmbianceLoop()
    {
        while (true)
        {
            transform.position = new Vector3(Random.Range(0, map.MapWidth), Random.Range(minHeight, maxHeight), Random.Range(0, map.MapWidth));
            source.Play();
            yield return new WaitForSeconds(source.clip.length + Random.Range(0, maxDelayBetweenTwo));
        }
    }
}
