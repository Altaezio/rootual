using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodProperties : MonoBehaviour
{
    public float weight = 1;
    public float collectTime = 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getWeight(){ return weight; }

    public float getCollectTime(){ return collectTime; }
}
