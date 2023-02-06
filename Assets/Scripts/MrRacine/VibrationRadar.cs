using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VibrationRadar : MonoBehaviour
{
    [SerializeField]
    private Transform otherPlayer;
    [SerializeField]
    private float angleToDetect;

    private void FixedUpdate()
    {
        float vibrationLvl = VibrationPerAngle(GetAngleBetweenPlayers());
        Debug.Log($"vibration {vibrationLvl}");
        SetControllerVibration.hightFrequency = vibrationLvl;
    }

    private float GetAngleBetweenPlayers()
    {
        return Vector3.Angle(transform.forward, otherPlayer.position - transform.position);
    }

    private float VibrationPerAngle(float angle)
    {
        Debug.Log($"angle : {angle}");
        if (angle > angleToDetect) return 0;
        return .3f * (-1 / angleToDetect * angle + 1);
    }
}
