using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSoundTest : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Root"))
        {
            transform.parent.position = new Vector3(Random.Range(-10f, 10f), 1, Random.Range(-10f, 10f));
        }
    }
}
