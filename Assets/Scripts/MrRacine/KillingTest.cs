using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KillingTest : MonoBehaviour
{
    public bool PlayerInRange { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerInRange = true;
            if (SettingManager.AtRangeVIbration)
            {
                SetControllerVibration.lowFrequency = .5f;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerInRange = false;
            SetControllerVibration.lowFrequency = 0;
        }
    }
}
