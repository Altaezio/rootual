using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMap : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public float scale = 20f;
    public float offsetX = 100f;
    public float offsetY = 100f;
    public GameObject[] objects;
    public float threshold = 0.5f;

    private void Start()
    {
        // Create the noise map
        float[,] noiseMap = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Use Perlin noise to generate the height value
                float xCoord = (float)x / width * scale + offsetX;
                float yCoord = (float)y / height * scale + offsetY;
                float noise = Mathf.PerlinNoise(xCoord, yCoord);

                // Assign the height value to the noise map
                noiseMap[x, y] = noise;
            }
        }

        // Use the noise map to place objects on the terrain
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Get the height value from the noise map
                float heightValue = noiseMap[x, y];

                // Place an object based on the height value
                if (heightValue > threshold)
                {
                    // Pick a random object from the list
                    int index = Random.Range(0, objects.Length);
                    GameObject obj = objects[index];

                    // Instantiate the object at the current position
                    Vector3 position = new Vector3(x, heightValue, y);
                    Instantiate(obj, position, Quaternion.identity);
                }
            }
        }
    }
}
