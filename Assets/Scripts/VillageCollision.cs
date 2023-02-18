using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VillageCollision : MonoBehaviour
{
    private int gameDuration = 60*12;
    [SerializeField] private TextMeshProUGUI durationTMP;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(decrementDuration), 1.0f, 1.0f);
    }

    private void decrementDuration()
    {
        gameDuration--;
        durationTMP.text = gameDuration/60 + ":" + gameDuration%60;

        if(gameDuration <= 0)
        {
            SceneManager.LoadScene("RootWin");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<FoodCollect>().currentLoad >= 30)
        {
            SceneManager.LoadScene("PickerWin");
        }
    }
}
