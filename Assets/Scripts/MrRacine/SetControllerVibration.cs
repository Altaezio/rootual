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
        if (Gamepad.all.Count <= 0) return;
        if(!SettingManager.DirectionVibration)
        {
            hightFrequency = 0;
        }
        if(!SettingManager.AtRangeVIbration)
        {
            lowFrequency = 0;
        }
        Gamepad.current.SetMotorSpeeds(lowFrequency, hightFrequency);
    }

    private void OnDestroy()
    {
        if(!(Input.GetJoystickNames().Length > 0)) return;
        Gamepad.current.SetMotorSpeeds(0, 0);
    }
}
