using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingMouth : MonoBehaviour
{
    [SerializeField] private PlayerMovement mrPropreMovement;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("AIR KO");
            SetControllerVibration.hightFrequency = 0;
            SetControllerVibration.lowFrequency = 0;
            mrPropreMovement.IsImmobilized(true); // si le jeux intègre plusieurs vies pour le joueurs, il faut ajouter une ligne pour dé-immobiliser Mr Propre.
            other.GetComponent<LifeManagment>().Lives -= 1;
        }
    }
}
