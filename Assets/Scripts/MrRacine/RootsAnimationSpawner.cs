using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootsAnimationSpawner : MonoBehaviour
{
    [SerializeField] private Transform rootParent;
    [SerializeField] private List<GameObject> rootAnimationPrefab = new();
    [SerializeField]
    private float minDelayBetweenRootSpawn, maxDelayBetweenRootSpawn,
        distanceThresholdForRandom, maxDistanceSpawnFromCenter,
        minRandomScale, maxRandomScale,
        alignedRandomRotationOffset;

    private Vector3 lastPosition;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnRootLoop), 0, Random.Range(minDelayBetweenRootSpawn, maxDelayBetweenRootSpawn));
    }

    private void SpawnRootLoop()
    {
        if (Vector3.Distance(lastPosition, transform.position) < distanceThresholdForRandom)
            SpawnCloseRoot();
        else
            SpawnAlignedRoot();
        lastPosition = transform.position;
    }

    private void SpawnCloseRoot()
    {
        Vector3 newPosition = new(transform.position.x + Random.Range(0, maxDistanceSpawnFromCenter), 0, transform.position.z + Random.Range(0, maxDistanceSpawnFromCenter));
        SpawnRoot(newPosition, Quaternion.Euler(0, Random.Range(0, 360f), 0));
    }

    private void SpawnAlignedRoot()
    {
        SpawnRoot(new Vector3(transform.position.x, 0, transform.position.z), Quaternion.FromToRotation(Vector3.forward, transform.position - lastPosition));
    }

    private void SpawnRoot(Vector3 position, Quaternion rotation)
    {
        GameObject root = Instantiate(rootAnimationPrefab[Random.Range(0, rootAnimationPrefab.Count)], position, rotation, rootParent);
        root.SetActive(true);
        root.transform.localScale = new Vector3(Mathf.Sign(Random.value-.5f),root.transform.localScale.y, root.transform.localScale.z);
    }
}
