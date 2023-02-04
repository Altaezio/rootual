using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KillingFloor : MonoBehaviour
{
    [SerializeField]
    private GameObject killingMouth;
    [SerializeField]
    private float coolDown;

    private bool onCoolDown;

    private void Start()
    {
        killingMouth.SetActive(false);
        onCoolDown = false;
    }

    public void Eat(InputAction.CallbackContext context)
    {
        if (onCoolDown || !context.performed) return;
        TryToEat();
    }

    private void TryToEat()
    {
        onCoolDown = true;
        killingMouth.SetActive(true);
        StartCoroutine(CoolDown());
    }

    private IEnumerator CoolDown()
    {
        killingMouth.SetActive(false);
        yield return new WaitForSeconds(coolDown);
        onCoolDown = false;
    }
}
