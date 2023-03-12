using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

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
        if (Gamepad.all.Count > 0)
        {
            controllerWarning.text = "*Controller connected";
            controllerWarning.color = Color.black;
        }
        else
        {
            controllerWarning.text = "*No controller connected";
            controllerWarning.color = Color.red;
        }
    }
}

