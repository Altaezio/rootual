using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementTransform : MonoBehaviour
{
    [SerializeField] private Vector2 posXZOffset, posYOffset, rotOffset, scaleOffset;
    [SerializeField] private bool posXBool, posYBool, posZBool, rotXBool, rotYBool, rotZBool;

    // Start is called before the first frame update
    void Start()
    {
        float posX = (posXBool ? 1 : 0) * Random.Range(posXZOffset[0], posXZOffset[1]);
        float posY = (posYBool ? 1 : 0) * Random.Range(posYOffset[0], posYOffset[1]);
        float posZ = (posZBool ? 1 : 0) * Random.Range(posXZOffset[0], posXZOffset[1]);
        this.transform.position += new Vector3(posX, posY, posZ);

        float rotX = (rotXBool ? 1 : 0) * Random.Range(rotOffset[0], rotOffset[1]);
        float rotY = (rotYBool ? 1 : 0) * Random.Range(rotOffset[0], rotOffset[1]);
        float rotZ = (rotZBool ? 1 : 0) * Random.Range(rotOffset[0], rotOffset[1]);
        this.transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);
        
        if(scaleOffset != Vector2.zero)
        {
            float scale = Random.Range(scaleOffset[0], scaleOffset[1]);
            this.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
