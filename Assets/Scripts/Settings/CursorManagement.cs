using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManagement : MonoBehaviour
{
    [SerializeField] private bool visible;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = visible;
    }
}
