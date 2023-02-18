using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Running : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement;
    [SerializeField]
    private float runningRate, loseSpeed, getBackSpeed, waitBeforeGettingBack, waitBeforeHidingFill;
    [SerializeField]
    private Image staminaFill;

    private int maxStamina;
    private float currentStamina;
    private float CurrentStamina
    {
        get => currentStamina;
        set
        {
            currentStamina = value;
            staminaFill.fillAmount = currentStamina / maxStamina;
        }
    }
    private bool CanRun
    {
        get => currentStamina > 0;
    }
    private bool isGettingBack;

    private Coroutine up, down, countDown;

    private void Start()
    {
        maxStamina = 10;
        CurrentStamina = maxStamina;
        // staminaFill.transform.parent.gameObject.SetActive(false);
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (CanRun && context.performed)
        {
            playerMovement.UpdateRelativeMoveSpeed(runningRate);
            if (down != null)
                StopCoroutine(down);
            down = StartCoroutine(LoseStamina());
        }

        if (!isGettingBack && context.canceled)
        {
            if (up != null)
                StopCoroutine(up);
            up = StartCoroutine(GetItBack());
        }
    }

    private IEnumerator LoseStamina()
    {
        if (up != null)
            StopCoroutine(up);
        if (countDown != null)
            StopCoroutine(countDown);
        isGettingBack = false;
        staminaFill.transform.parent.gameObject.SetActive(true);
        while (CurrentStamina > 0)
        {
            CurrentStamina -= Time.deltaTime * loseSpeed;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        up = StartCoroutine(GetItBack());
    }

    private IEnumerator GetItBack()
    {
        if (down != null)
            StopCoroutine(down);
        isGettingBack = true;
        playerMovement.UpdateRelativeMoveSpeed(1/runningRate);
        yield return new WaitForSeconds(waitBeforeGettingBack);
        while (CurrentStamina < maxStamina)
        {
            CurrentStamina += Time.deltaTime * getBackSpeed;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        CurrentStamina = maxStamina;
        isGettingBack = false;
        if(countDown != null)
            StopCoroutine(countDown);
        // countDown = StartCoroutine(CountDownToHideFill());
    }

    private IEnumerator CountDownToHideFill()
    {
        yield return new WaitForSeconds(waitBeforeHidingFill);
        staminaFill.transform.parent.gameObject.SetActive(false);
    }
}
