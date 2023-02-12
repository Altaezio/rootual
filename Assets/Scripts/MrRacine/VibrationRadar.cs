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
    [SerializeField]
    private float vibration;

    private void FixedUpdate()
    {
        float vibrationLvl = VibrationPerAngle(GetAngleBetweenPlayers());
        SetControllerVibration.hightFrequency = vibrationLvl;
    }

    private float GetAngleBetweenPlayers()
    {
        return Vector3.Angle(transform.forward, otherPlayer.position - transform.position);
    }

    private float VibrationPerAngle(float angle)
    {
        if (angle > angleToDetect) return 0;
        return vibration;
    }
}
