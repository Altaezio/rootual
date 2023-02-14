using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public GameObject CollectBar;
    public Animator CollectBarAnim;

    [SerializeField]
    private List<GameObject> bigPlants, middlePlants, smallPlants, nothing, rocks;
    [SerializeField]
    private float scale;
    [SerializeField]
    private int width;
    [SerializeField]
    private float spawnoffset; // random offset when spawning objectfs so they are not aligned
    [SerializeField]
    private float defaultInstantiateThreshold;
    [SerializeField]
    private GameObject rescueZone, mrPropre, mrRacine;
    [SerializeField]
    private Transform rockParent;

    private List<List<GameObject>> objectPrefabs = new();
    private int[,] matrixMap;
    private int seed;

    private void Start()
    {
        objectPrefabs.Add(middlePlants);
        objectPrefabs.Add(bigPlants);
        objectPrefabs.Add(smallPlants);
        objectPrefabs.Add(nothing);

        GenerateMap();
        SpawnEntities();
    }

    private void GenerateMap()
    {
        matrixMap = new int[width, width];

        seed = Random.Range(0, 10000);
        Random.InitState(seed);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < width; z++)
            {
                float noise = Mathf.PerlinNoise((x + seed) / scale, (z + seed) / scale);
                int objectIndex = Mathf.Clamp(Mathf.FloorToInt(noise * objectPrefabs.Count), 0, objectPrefabs.Count - 1);
                matrixMap[x, z] = objectIndex;
                List<GameObject> objects = objectPrefabs[objectIndex];
                GameObject objectPrefab = objects[Random.Range(0, objects.Count)];

                Vector3 position = new(x + Random.Range(-1f, 1f) * spawnoffset, 0, z + Random.Range(-1f, 1f) * spawnoffset);

                float probability = Random.Range(0.0f, 1.0f);
                float instantiateThreshold;

                switch (objectIndex)
                {
                    case 3:
                        instantiateThreshold = 1.0f;
                        break;
                    case 0:
                    case 6:
                        instantiateThreshold = 0.5f;
                        break;
                    case 2:
                    case 4:
                        instantiateThreshold = 0.25f;
                        break;
                    default:
                        instantiateThreshold = defaultInstantiateThreshold;
                        break;
                }

                if (probability > 1 - instantiateThreshold)
                {
                    GameObject clone = Instantiate(objectPrefab, position, Quaternion.Euler(0, Random.Range(0, 180), 0), transform);
                }
            }
        }

        // Rocks
        int lowerBound = -3;
        int higherBound = width + 2;
        for (int x = lowerBound; x < higherBound; x++)
        {
            SpawnRock(x, lowerBound);
            SpawnRock(x, higherBound);
        }
        for (int z = lowerBound + 1; z < higherBound - 1; z++)
        {
            SpawnRock(lowerBound, z);
            SpawnRock(higherBound, z);
        }

        // Far Trees
        int padding = 1;
        for (int x = lowerBound - padding; x < higherBound + padding; x++)
        {
            SpawnFarTrees(x, lowerBound - padding);
            SpawnFarTrees(x, lowerBound - padding - 1);
            SpawnFarTrees(x, higherBound + padding);
            SpawnFarTrees(x, higherBound + padding + 1);
        }
        for (int z = lowerBound - padding + 1; z < higherBound + padding - 1; z++)
        {
            SpawnFarTrees(lowerBound - padding, z);
            SpawnFarTrees(lowerBound - padding - 1, z);
            SpawnFarTrees(higherBound + padding, z);
            SpawnFarTrees(higherBound + padding + 1, z);
        }

        /* int best = 0;
        int bestX = 0;
        int bestZ = 0;

        int nTest = width;

        for (int i = 0; i < nTest * nTest; i++)
        {
            int randX = Random.Range(0, width - villageWidth);
            int randZ = Random.Range(0, width - villageWidth);

            int sum = 0;

            for (int x = randX; x < randX + villageWidth; x++)
            {
                for (int z = randZ; z < randZ + villageWidth; z++)
                {
                    if (matrixMap[x, z] == 3)
                    {
                        sum += 1;
                    }
                }
            }

            if (sum > best)
            {
                best = sum;
                bestX = randX;
                bestZ = randZ;
            }
        } */
    }

    private void SpawnEntities()
    {
        int mrPropreX;
        int mrPropreZ;
        do
        {
            mrPropreX = Random.Range(0, width);
            mrPropreZ = Random.Range(0, width);
        } while (matrixMap[mrPropreX, mrPropreZ] != objectPrefabs.IndexOf(nothing));

        mrPropre.transform.position = new Vector3(mrPropreX, mrPropre.transform.position.y, mrPropreZ);
        rescueZone.transform.position = new Vector3(mrPropreX, rescueZone.transform.position.y, mrPropreZ);

        Vector3 mrRacinePos;
        do
        {
            mrRacinePos = new Vector3(Random.Range(0, width), mrRacine.transform.position.y, Random.Range(0, width));
        } while (Vector3.Distance(mrPropre.transform.position, mrRacinePos) < (width / 2) - 1);

        mrRacine.transform.position = mrRacinePos;
    }

    private void SpawnRock(int x, int z)
    {
        SpawnAThing(x, z, rocks[Random.Range(0, rocks.Count)]);
    }
 
    private void SpawnFarTrees(int x, int z)
    {
        SpawnAThing(x, z, bigPlants[Random.Range(0, bigPlants.Count)]);
    }

    private void SpawnAThing(int x, int z, GameObject prefab)
    {
        Vector3 position = new(x + Random.Range(-1f, 1f) * spawnoffset, Random.Range(-1f, 1f) * spawnoffset, z + Random.Range(-1f, 1f) * spawnoffset);
        Instantiate(prefab, position, Quaternion.Euler(0, Random.Range(0, 180), 0), rockParent);
    }
}
