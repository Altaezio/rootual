using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToLook;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + objectToLook.transform.rotation * Vector3.forward, objectToLook.transform.rotation * Vector3.up);
    }
}
