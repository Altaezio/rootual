using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingMouth : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("AIR KO");
            other.GetComponent<LifeManagment>().Lives -= 1;
        }
    }
}
