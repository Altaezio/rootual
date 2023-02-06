using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> bigPlants;
    [SerializeField]
    private List<GameObject> middlePlants;
    [SerializeField]
    private List<GameObject> smallPlants;
    [SerializeField]
    private List<GameObject> nothing;
    [SerializeField]
    public GameObject CollectBar;
    public Animator CollectBarAnim;
    private List<List<GameObject>> objectPrefabs = new();
    public int[,] matrixMap;
    public float scale = 10f;
    private int seed;
    public int width = 20;
    public float defaultInstantiateThreshold = 0.5f;
    public GameObject rescueZone, mrPropre, mrRacine;
    

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

                Vector3 position = new Vector3(x, 0, z);

                float probability = Random.Range(0.0f, 1.0f);
                float instantiateThreshold;

                switch (objectIndex)
                {
                    case 3:
                        instantiateThreshold = 1.0f;
                        break;
                    case 0: case 6:
                        instantiateThreshold = 0.5f;
                        break;
                    case 2: case 4:
                        instantiateThreshold = 0.25f;
                        break;
                    default:
                        instantiateThreshold = defaultInstantiateThreshold;
                        break;
                }

                if (probability > 1 - instantiateThreshold)
                {
                    GameObject clone = Instantiate(objectPrefab, position, Quaternion.identity, this.transform);
                }
            }
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
        do{
            mrPropreX = Random.Range(0, width);
            mrPropreZ = Random.Range(0, width);
        }while(matrixMap[mrPropreX, mrPropreZ] != objectPrefabs.IndexOf(nothing));

        mrPropre.transform.position = new Vector3(mrPropreX, mrPropre.transform.position.y, mrPropreZ);
        rescueZone.transform.position = new Vector3(mrPropreX, rescueZone.transform.position.y, mrPropreZ);

        Vector3 mrRacinePos;
        do{
            mrRacinePos = new Vector3(Random.Range(0, width), mrRacine.transform.position.y, Random.Range(0, width));
        }while(Vector3.Distance(mrPropre.transform.position, mrRacinePos) < (width/2)-1);

        mrRacine.transform.position = mrRacinePos;
    }
}
