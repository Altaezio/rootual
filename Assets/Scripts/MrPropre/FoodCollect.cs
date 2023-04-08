using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class FoodCollect : MonoBehaviour
{
    [SerializeField] private List<GameObject> foodInPocket = new List<GameObject>();
    [SerializeField] private List<GameObject> foodsInRange = new List<GameObject>();
    public GameObject foodPocket;
    [SerializeField] private PlayerMovement mrPropreMovement;
    [SerializeField] private MrPropreAnim mrPropreAnim;
    [SerializeField] private float maxLoad;
    public float currentLoad;
    [SerializeField] private Image fillFoodBar;
    [SerializeField] private TextMeshProUGUI loadTmp;

    private void Start()
    {
        maxLoad = SettingManager.FruitAmountNeeded;
        currentLoad = 0;
    }
    
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

    public void DropFood(InputAction.CallbackContext context)
    {
        if (foodInPocket.Count == 0 || !context.performed) return;
        GameObject lastFood = foodInPocket[^1];
        float lastFoodWeight = lastFood.GetComponent<FoodProperties>().Weight;

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
        loadTmp.text = currentLoad + " / 30";
        float speed = Mathf.Clamp(((mrPropreMovement.MinMoveSpeed - mrPropreMovement.DefaultMoveSpeed) * currentLoad) / maxLoad + mrPropreMovement.DefaultMoveSpeed, mrPropreMovement.MinMoveSpeed, mrPropreMovement.DefaultMoveSpeed);
        mrPropreMovement.UpdateMoveSpeed(speed);
    }

    private void CheckMaxLoad()
    {
        fillFoodBar.color = (currentLoad >= maxLoad) ? new Color(1.0f, 117/255f, 0.0f, 1.0f) : new Color(146/255f, 208/255f, 80/255f, 1.0f);
    }

    public bool IsFull(){ return currentLoad >= maxLoad; }
}
