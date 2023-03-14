using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGame : MonoBehaviour
{
    private int gameDuration = 60*12;
    [SerializeField] private TextMeshProUGUI durationTMP;

    // Start is called before the first frame update
    void Start()
    {
        gameDuration = SettingManager.TimeLimit;
        InvokeRepeating(nameof(DecrementDuration), 1.0f, 1.0f);
    }

    private void DecrementDuration()
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
        if(other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<FoodCollect>().IsFull())
        {
            SceneManager.LoadScene("PickerWin");
        }
    }
}
