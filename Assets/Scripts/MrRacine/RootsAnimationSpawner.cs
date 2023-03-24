using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootsAnimationSpawner : MonoBehaviour
{
    [SerializeField] private Transform rootParent;
    [SerializeField] private List<GameObject> rootAnimationPrefab = new();
    [SerializeField] private float minDelayBetweenRootSpawn, maxDelayBetweenRootSpawn,
        distanceThresholdForRandom, maxDistanceSpawnFromCenter,
        minRandomScale, maxRandomScale,
        alignedRandomRotationOffset;
    private Vector3 lastPosition;
    private int rootNb = 8;
    [SerializeField] private List<GameObject> rootList = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < rootNb; i++)
        {
            GameObject root;
            if(i % 2 == 0){
                root = Instantiate(rootAnimationPrefab[0], rootParent);
            }else{
                root = Instantiate(rootAnimationPrefab[1], rootParent);
            }
            root.SetActive(false);
            rootList.Add(root);
        }

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
        // GameObject root = Instantiate(rootAnimationPrefab[Random.Range(0, rootAnimationPrefab.Count)], position, rotation, rootParent);
        GameObject root = null;
        for (int i = 0; i < rootList.Count; i++)
        {
            if(!rootList[i].activeSelf){
                root = rootList[i];
            }
        }

        if(root != null){
            root.transform.position = position;
            root.transform.rotation = rotation;
            root.transform.localScale = new Vector3(Mathf.Sign(Random.value-.5f),root.transform.localScale.y, root.transform.localScale.z);
            root.SetActive(true);
        }
    }
}
