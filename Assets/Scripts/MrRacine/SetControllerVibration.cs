using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SetControllerVibration : MonoBehaviour
{
    public static float lowFrequency;
    public static float hightFrequency;

    private void FixedUpdate()
    {
        if(!(Input.GetJoystickNames().Length > 0)) return;
        Gamepad.current.SetMotorSpeeds(lowFrequency, hightFrequency);
    }

    private void OnDestroy()
    {
        Gamepad.current.SetMotorSpeeds(0, 0);
    }
}
