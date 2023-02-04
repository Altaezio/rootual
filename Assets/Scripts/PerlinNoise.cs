using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public GameObject village;
    public int villageWidth;
    public int[,] matrixMap;
    public float scale = 10f;
    private int seed;
    public int width = 20;
    public float defaultInstantiateThreshold = 0.5f;
    
    private void Start()
    {
        matrixMap = new int[width, width];
        villageWidth = 10;

        seed = Random.Range(0, 10000);
        Debug.Log(seed);
        Random.InitState(seed);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < width; z++)
            {
                float noise = Mathf.PerlinNoise((x + seed) / scale, (z + seed) / scale);
                int objectIndex = Mathf.Clamp(Mathf.FloorToInt(noise * objectPrefabs.Length), 0, objectPrefabs.Length-1);
                Debug.Log(objectIndex);
                matrixMap[x,z] = objectIndex;
                GameObject objectPrefab = objectPrefabs[objectIndex];

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

                if(probability > 1 - instantiateThreshold){
                    GameObject clone = Instantiate(objectPrefab, position, Quaternion.identity);
                    if(Random.Range(0.0f, 1.0f) > 0.2f)
                    {
                        try
                        {
                            GameObject child = clone.transform.GetChild(0).gameObject;
                            if(child.CompareTag("food")){
                                Destroy(child);
                            }
                        }
                        catch (System.Exception){ }
                    } 
                }
            }
        }

        int best = 0;
        int bestX = 0;
        int bestZ = 0;

        int nTest = width;

        for (int i = 0; i < nTest*nTest; i++)
        {
            int randX = Random.Range(0, width - villageWidth);
            int randZ = Random.Range(0, width - villageWidth);

            int sum = 0;
            
            for(int x = randX; x < randX + villageWidth; x++)
            {
                for(int z = randZ; z < randZ + villageWidth; z++)
                {
                    // Debug.Log("Test");
                    if(matrixMap[x,z] == 3)
                    {
                        sum += 1;
                        // Debug.Log(sum);
                    }
                }
            }

            if(sum > best)
            {
                best = sum;
                bestX = randX;
                bestZ = randZ;
            }
        }

        Instantiate(village, new Vector3(bestX + villageWidth, 0, bestZ + villageWidth), Quaternion.identity);

        Debug.Log("best : " + best);
        Debug.Log("x : " + bestX);
        Debug.Log("z : " + bestZ);
    }
}
