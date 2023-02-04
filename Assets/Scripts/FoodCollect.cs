using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCollect : MonoBehaviour
{
    private FirstPersonMovement firstPersonMovement;
    public List<GameObject> foodObject = new List<GameObject>();
    public GameObject foodPocket;
    [SerializeField] private float load = 30;
    private float timer = 0f;
    private bool holdingMouse = false;
    public AudioClip sound;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        firstPersonMovement = GetComponent<FirstPersonMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        dropFood();
    }

    private void dropFood()
    {
        if(Input.GetMouseButtonDown(1) && foodObject.Count > 0)
        {
            GameObject lastFood = foodObject[foodObject.Count-1];
            float lastFoodWeight = lastFood.GetComponent<FoodProperties>().getWeight();

            lastFood.transform.parent = null;
            lastFood.SetActive(true);
            foodObject.Remove(lastFood);

            calculateSpeed(lastFoodWeight);
        }
    }

    private bool holdMouseToCollect(float collectTime)
    {        
        if (Input.GetMouseButtonDown(0))
        {
            holdingMouse = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            holdingMouse = false;
            timer = 0f;
        }

        if (holdingMouse)
        {
            timer += Time.deltaTime;

            if (timer >= collectTime)
            {
                return true;
            }
        }
        return false;
    }

    /*IEnumerator SoundOut()
    {
        if(!audioSource.isPlaying){
            audioSource.PlayOneShot(sound);
        }  
        yield return new WaitForSeconds(1f);
    }*/

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("food"))
        {
            GameObject newFood = other.gameObject;
            float newFoodWeight = newFood.GetComponent<FoodProperties>().getWeight();
            float newCollectTime = newFood.GetComponent<FoodProperties>().getCollectTime();
            
            if(holdMouseToCollect(newCollectTime) && (load - newFoodWeight) >= 0)
            {
                foodObject.Add(newFood);
                newFood.SetActive(false);
                newFood.transform.parent = foodPocket.transform;
                
                calculateSpeed(-newFoodWeight);
            }
        }
    }

    private void OnTriggerEnter()
    {
        timer = 0f;
        holdingMouse = false;
    }

    private void calculateSpeed(float foodLoad)
    {
        load += foodLoad;
        float speed = (4*load)/30 + 1;
        firstPersonMovement.updateSpeed(speed);
    }
}
