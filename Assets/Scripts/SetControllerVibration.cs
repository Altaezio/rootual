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
        Gamepad.current.SetMotorSpeeds(lowFrequency, hightFrequency);
    }
}
