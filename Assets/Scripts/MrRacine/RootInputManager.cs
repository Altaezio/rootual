using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RootInputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput rootInput;
    private bool previousCheck;

    // Start is called before the first frame update
    void Start()
    {
        previousCheck = false;
        InvokeRepeating(nameof(CheckController), 0, 1);
    }

    private void CheckController()
    {
        if (Gamepad.all.Count > 0 && !previousCheck){
            rootInput.enabled = true;
            previousCheck = true;
        } 
        else if (Gamepad.all.Count <= 0 && previousCheck) 
        {
            rootInput.enabled = false;
            previousCheck = false;
        }
    }
}
