using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FoodCollect : MonoBehaviour
{
    public List<GameObject> foodObject = new List<GameObject>();
    public GameObject foodPocket;

    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private float maxLoad;

    public float currentLoad;
    private bool foodAtRange;
    private GameObject foodInRange;
    public GameObject fillFoodBar;

    private void Start()
    {
        currentLoad = maxLoad;
    }

    public void DropFood(InputAction.CallbackContext context)
    {
        if (foodObject.Count == 0 || !context.performed) return;
        GameObject lastFood = foodObject[^1];
        float lastFoodWeight = lastFood.GetComponent<FoodProperties>().Weight;

        //lastFood.transform.localPosition = Vector3.zero; maybe to change position when you drop
        lastFood.transform.parent = null;
        lastFood.GetComponent<Rigidbody>().isKinematic = false;
        lastFood.GetComponent<FoodProperties>().CollectTime = 0.5f;
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
            foodInRange.GetComponent<FoodProperties>().StoppedCollected();
            foodAtRange = false;
            if (foodInRange != null)
                foodInRange.GetComponent<FoodProperties>().StoppedCollected();
            foodInRange = null;
        }
    }

    public void Collect(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            StopCoroutine(nameof(Collecting));
            if (foodAtRange)
            {
                foodInRange.GetComponent<FoodProperties>().StoppedCollected();
            }
        }

        if (!foodAtRange) return;

        if (context.performed)
            StartCoroutine(nameof(Collecting));
    }

    private IEnumerator Collecting()
    {
        FoodProperties foodProperties = foodInRange.GetComponent<FoodProperties>();
        float newFoodWeight = foodProperties.Weight;

        if (currentLoad - newFoodWeight < 0) yield break;

        foodProperties.IsCollected();

        yield return new WaitForSecondsRealtime(foodProperties.CollectTime);

        foodProperties.StoppedCollected();

        if (foodAtRange)
        {
            foodObject.Add(foodInRange);
            foodInRange.SetActive(false);
            foodInRange.transform.parent = foodPocket.transform;
            foodInRange.transform.position = foodPocket.transform.position;
            foodInRange.GetComponent<Rigidbody>().isKinematic = true;

            CalculateSpeed(-newFoodWeight);
        }
    }

    private void CalculateSpeed(float foodLoad)
    {
        currentLoad += foodLoad;
        fillFoodBar.GetComponent<Image>().fillAmount = 1 - (currentLoad / maxLoad);
        float speed = (((playerMovement.DefaultMoveSpeed - playerMovement.MinMoveSpeed) * currentLoad) / maxLoad + playerMovement.MinMoveSpeed);
        playerMovement.UpdateMoveSpeed(speed);
    }
}
