using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class FoodCollect : MonoBehaviour
{
    public List<GameObject> foodObject = new List<GameObject>();
    public GameObject foodPocket;
    public AudioClip sound;

    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private float maxLoad;

    private float currentLoad;
    private bool foodAtRange;
    private GameObject foodInRange;

    private void Start()
    {
        currentLoad = maxLoad;
    }

    public void DropFood()
    {
        if (foodObject.Count == 0) return;
        GameObject lastFood = foodObject[^1];
        float lastFoodWeight = lastFood.GetComponent<FoodProperties>().Weight;

        //lastFood.transform.localPosition = Vector3.zero; maybe to change position when you drop
        lastFood.transform.parent = null;
        lastFood.SetActive(true);
        foodObject.Remove(lastFood);

        CalculateSpeed(lastFoodWeight);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("food"))
        {
            foodAtRange = true;
            foodInRange = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("food"))
        {
            foodAtRange = false;
            foodInRange = null;
        }
    }

    public void Collect(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            StopCoroutine(nameof(Collecting));
            foodInRange.GetComponent<FoodProperties>().StoppedCollected();
        }

            if (!foodAtRange) return;

        if (context.performed)
            StartCoroutine(nameof(Collecting));
    }

    private IEnumerator Collecting()
    {
        FoodProperties foodProperties = foodInRange.GetComponent<FoodProperties>();
        float newFoodWeight = foodProperties.Weight;
        float newCollectTime = foodProperties.CollectTime;

        if (currentLoad - newFoodWeight < 0) yield break;

        foodProperties.IsCollected();

        while (foodAtRange && newCollectTime>= 0)
        {
            newCollectTime -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        foodProperties.StoppedCollected();

        foodObject.Add(foodInRange);
        foodInRange.SetActive(false);
        foodInRange.transform.parent = foodPocket.transform;
        
        CalculateSpeed(-newFoodWeight);
    }

    private void CalculateSpeed(float foodLoad)
    {
        currentLoad += foodLoad;
        float speed = (((playerMovement.DefaultMoveSpeed - 1) * currentLoad) / maxLoad + 1) / 30;
        playerMovement.UpdateMoveSpeed(speed);
    }
}
