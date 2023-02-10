using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FoodCollect : MonoBehaviour
{
    public List<GameObject> foodObject = new List<GameObject>();
    public GameObject foodPocket;

    [SerializeField]
    private PlayerMovement mrPropreMovement;
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

    // À noter qu'avec OnTriggerEnter, si le joueur pose 2 objets l'un sur l'autre, il ne peut prendre qu'un objet dans un premier temps (logique) puis pour prendre le second, il doit sortir de son rayon et entrer de nouveau. Problème résolu avec OnTriggerStay mais plein de request envoyée quand le joueur est dans la zone d'un fruit.
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("food"))
        {
            // Debug.Log(other.gameObject.name);
            foodAtRange = true;
            foodInRange = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("food"))
        {
            EndCollecting();
        }
    }

    private void EndCollecting(){
        foodAtRange = false;
        if (foodInRange != null)
            foodInRange.GetComponent<FoodProperties>().StoppedCollected();
        foodInRange = null;
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

            EndCollecting();
        }
    }

    private void CalculateSpeed(float foodLoad)
    {
        currentLoad += foodLoad;
        fillFoodBar.GetComponent<Image>().fillAmount = 1 - (currentLoad / maxLoad);
        float speed = (((mrPropreMovement.DefaultMoveSpeed - mrPropreMovement.MinMoveSpeed) * currentLoad) / maxLoad + mrPropreMovement.MinMoveSpeed);
        mrPropreMovement.UpdateMoveSpeed(speed);
    }
}
