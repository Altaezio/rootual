using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerWarning : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI controllerWarning;

    // Update is called once per frame
    void Update()
    {
        DetectController();
    }

    public void DetectController()
    {
        foreach (string input in Input.GetJoystickNames())
        {
            if(input.ToLower().Contains("wireless controller"))
            {
                controllerWarning.text = "*Controller connected";
                controllerWarning.color = Color.black;
            } else {
                controllerWarning.text = "*No controller connected";
                controllerWarning.color = Color.red;
            }
        }
    }
}
