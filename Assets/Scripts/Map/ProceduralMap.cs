using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMap : MonoBehaviour
{
    public GameObject CollectBar;
    public Animator CollectBarAnim;
    [SerializeField] private List<GameObject> bigPlants, middlePlants, smallPlants, nothing, rocks, borderTrees;
    [SerializeField] private float scale;
    [SerializeField] private int width;
    [SerializeField] private float spawnOffset; // random offset when spawning objectifs so they are not aligned
    [SerializeField] private float defaultInstantiateThreshold;
    [SerializeField] private GameObject rescueZone, mrPropre, mrRacine;
    [SerializeField] private Transform borderParent;
    private List<List<GameObject>> objectPrefabs = new();
    private int[,] matrixMap;
    private int seed;
    private bool campFireSpawn;
    public int MapWidth { get => width; }

    private void Start()
    {
        campFireSpawn = SettingManager.SpawnAtFireCamp;

        objectPrefabs.Add(middlePlants);
        objectPrefabs.Add(bigPlants);
        objectPrefabs.Add(smallPlants);
        objectPrefabs.Add(nothing);

        GenerateMap();
        SpawnBorders();
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

                Vector3 position = new(x + Random.Range(-1f, 1f) * spawnOffset, 0, z + Random.Range(-1f, 1f) * spawnOffset);

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
    }

    private void SpawnBorders()
    {
        // Rocks
        float rockStep = 1.3f; // espacement entre 2 rochers
        int lowerBound = -2;
        int higherBound = width + 2;
        for (float x = lowerBound; x < higherBound; x += rockStep)
        {
            SpawnElement(x, lowerBound, rocks[Random.Range(0, rocks.Count)]);
            SpawnElement(x, higherBound, rocks[Random.Range(0, rocks.Count)]);
        }
        for (float z = lowerBound + 1; z < higherBound - 1; z += rockStep)
        {
            SpawnElement(lowerBound, z, rocks[Random.Range(0, rocks.Count)]);
            SpawnElement(higherBound, z, rocks[Random.Range(0, rocks.Count)]);
        }

        // Far Trees
        int padding = 1; // espacement rocher/arbre
        int treeStep = 2; // espacement entre 2 arbres
        for (int x = lowerBound - padding; x < higherBound + padding; x += treeStep)
        {
            SpawnElement(x, lowerBound - padding, borderTrees[Random.Range(0, borderTrees.Count)]);
            SpawnElement(x - treeStep, lowerBound - padding - 1, borderTrees[Random.Range(0, borderTrees.Count)]);
            SpawnElement(x, higherBound + padding, borderTrees[Random.Range(0, borderTrees.Count)]);
            SpawnElement(x + treeStep, higherBound + padding + 1, borderTrees[Random.Range(0, borderTrees.Count)]);
        }
        for (int z = lowerBound - padding + 1; z < higherBound + padding - 1; z += treeStep)
        {
            SpawnElement(lowerBound - padding, z, borderTrees[Random.Range(0, borderTrees.Count)]);
            SpawnElement(lowerBound - padding - 1, z - treeStep, borderTrees[Random.Range(0, borderTrees.Count)]);
            SpawnElement(higherBound + padding, z, borderTrees[Random.Range(0, borderTrees.Count)]);
            SpawnElement(higherBound + padding + 1, z + treeStep, borderTrees[Random.Range(0, borderTrees.Count)]);
        }
    }

    private void SpawnElement(float x, float z, GameObject prefab)
    {
        Instantiate(prefab, new Vector3(x, 0, z), Quaternion.identity, borderParent);
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

        if(campFireSpawn)
        {
            rescueZone.transform.position = new Vector3(mrPropreX, rescueZone.transform.position.y, mrPropreZ);
        }else{
            randSpawnDis((width / 3), rescueZone);      
        }

        randSpawnDis((width / 2), mrRacine);        
    }

    private void randSpawnDis(float maxDistance, GameObject entity)
    {
        Vector3 pos;
        do
        {
            pos = new Vector3(Random.Range(0, width), entity.transform.position.y, Random.Range(0, width));
        } while (Vector3.Distance(mrPropre.transform.position, pos) < maxDistance - 1);

        entity.transform.position = pos;
    }
}
