using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FoodCollect : MonoBehaviour
{
    [SerializeField] private List<GameObject> foodInPocket = new List<GameObject>();
    [SerializeField] private List<GameObject> foodsInRange = new List<GameObject>();
    public GameObject foodPocket;
    [SerializeField] private PlayerMovement mrPropreMovement;
    [SerializeField] private MrPropreAnim mrPropreAnim;
    [SerializeField] private float maxLoad;
    public float currentLoad;
    // private bool foodAtRange;
    // private GameObject foodInRange;
    [SerializeField] private Image fillFoodBar;

    private void Start()
    {
        currentLoad = 0;
    }

    // Ancien code
    // À noter qu'avec OnTriggerEnter, si le joueur pose 2 objets l'un sur l'autre, il ne peut prendre qu'un objet dans un premier temps (logique) puis pour prendre le second, il doit sortir de son rayon et entrer de nouveau. Problème résolu avec OnTriggerStay mais plein de request envoyée quand le joueur est dans la zone d'un fruit.
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("food"))
        {
            // Debug.Log(other.gameObject.name);
            foodAtRange = true;
            foodInRange = other.gameObject;
        }
    }

    // Ancien code
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("food"))
        {
            EndCollecting();
        }
    }*/

    // Ancien code
    /*private void EndCollecting()
    {
        foodAtRange = false;
        if (foodInRange != null)
            foodInRange.GetComponent<FoodProperties>().StopCollectAnim();
        foodInRange = null;
    }*/
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("food"))
        {
            foodsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("food"))
        {
            foodsInRange.Remove(other.gameObject);
            other.GetComponent<FoodProperties>().StopCollectAnim();
            mrPropreAnim.StopPickUpAnim();
        }
    }

    // Ancien code
    /*public void Collect(InputAction.CallbackContext context)
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
    }*/

    public void Collect(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            StopCoroutine(nameof(Collecting));
            if (foodsInRange.Count > 0)
            {
                foodsInRange[0].GetComponent<FoodProperties>().StopCollectAnim();
                mrPropreAnim.StopPickUpAnim();
            }
        }

        if (!(foodsInRange.Count > 0)) return;

        if (context.performed)
            StartCoroutine(nameof(Collecting));
    }

    private IEnumerator Collecting()
    {
        GameObject newFood = foodsInRange[0];
        FoodProperties foodProperties = newFood.GetComponent<FoodProperties>();

        foodProperties.CollectAnim();
        mrPropreAnim.PickUpAnim(foodProperties.FoodType.ToString());

        yield return new WaitForSecondsRealtime(foodProperties.CollectTime);

        foodProperties.StopCollectAnim();
        mrPropreAnim.StopPickUpAnim();

        foodInPocket.Add(newFood);
        foodsInRange.Remove(newFood);
        newFood.SetActive(false);
        newFood.transform.parent = foodPocket.transform;
        newFood.transform.position = foodPocket.transform.position;
        newFood.GetComponent<Rigidbody>().isKinematic = true;

        CalculateSpeed(foodProperties.Weight);
    }

    // Ancien code
    /*private IEnumerator Collecting()
    {
        FoodProperties foodProperties = foodInRange.GetComponent<FoodProperties>();
        float newFoodWeight = foodProperties.Weight;

        // if (currentLoad - newFoodWeight < 0) yield break;

        foodProperties.IsCollected();
        mrPropreAnim.PickFoodAnim(foodProperties.FoodType);

        yield return new WaitForSecondsRealtime(foodProperties.CollectTime);

        foodProperties.StoppedCollected();

        if (foodAtRange)
        {
            foodObject.Add(foodInRange);
            foodInRange.SetActive(false);
            foodInRange.transform.parent = foodPocket.transform;
            foodInRange.transform.position = foodPocket.transform.position;
            foodInRange.GetComponent<Rigidbody>().isKinematic = true;

            CalculateSpeed(newFoodWeight);

            EndCollecting();
        }
    }*/

    public void DropFood(InputAction.CallbackContext context)
    {
        if (foodInPocket.Count == 0 || !context.performed) return;
        GameObject lastFood = foodInPocket[^1];
        float lastFoodWeight = lastFood.GetComponent<FoodProperties>().Weight;

        //lastFood.transform.localPosition = Vector3.zero; maybe to change position when you drop
        lastFood.transform.parent = null;
        lastFood.GetComponent<Rigidbody>().isKinematic = false;
        lastFood.GetComponent<FoodProperties>().CollectTime = 0.5f;
        lastFood.SetActive(true);
        foodInPocket.Remove(lastFood);

        CalculateSpeed(-lastFoodWeight);
    }

    private void CalculateSpeed(float foodLoad)
    {
        currentLoad += foodLoad;
        CheckMaxLoad();

        fillFoodBar.fillAmount = currentLoad / maxLoad;
        float speed = Mathf.Clamp(((mrPropreMovement.MinMoveSpeed - mrPropreMovement.DefaultMoveSpeed) * currentLoad) / maxLoad + mrPropreMovement.DefaultMoveSpeed, mrPropreMovement.MinMoveSpeed, mrPropreMovement.DefaultMoveSpeed);
        mrPropreMovement.UpdateMoveSpeed(speed);
    }

    private void CheckMaxLoad()
    {
        fillFoodBar.color = (currentLoad >= maxLoad) ? new Color(1.0f, 117/255f, 0.0f, 1.0f) : new Color(146/255f, 208/255f, 80/255f, 1.0f);
    }

    public bool IsFull(){ return currentLoad >= maxLoad; }
}
