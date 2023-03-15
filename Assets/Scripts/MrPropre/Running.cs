using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Running : MonoBehaviour
{
    public event Action<bool> ChangedSpeedToRunning;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float runningRate, loseSpeed, getBackSpeed, waitBeforeGettingBack;
    [SerializeField] private Image staminaFill;
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
        get => currentStamina > 0 && isMoving;
    }

    private bool isMoving;

    private int updateDirectionStamina;

    private Coroutine updateStamina, changeDirectionStamina;

    private void Start()
    {
        maxStamina = 10;
        CurrentStamina = maxStamina;
        updateDirectionStamina = 1;
    }

    public void Run(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartRunning();
        }

        if (context.canceled)
        {
            StopRunning();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        isMoving = context.ReadValue<Vector2>().sqrMagnitude > 0.1f;
        if (!isMoving)
        {
            StopRunning();
        }
    }

    private void StartRunning()
    {
        if (!CanRun || updateDirectionStamina < 0) return;

        playerMovement.UpdateRelativeMoveSpeed(runningRate);
        ChangedSpeedToRunning(true);

        if (changeDirectionStamina != null)
            StopCoroutine(changeDirectionStamina);
        changeDirectionStamina = StartCoroutine(ChangeDirectionUpdate(false));
    }

    private void StopRunning()
    {
        if (updateDirectionStamina >= 0) return;

        playerMovement.UpdateRelativeMoveSpeed(1 / runningRate);
        ChangedSpeedToRunning(false);

        if (changeDirectionStamina != null)
            StopCoroutine(changeDirectionStamina);
        changeDirectionStamina = StartCoroutine(ChangeDirectionUpdate(true));
    }

    private IEnumerator ChangeDirectionUpdate(bool shouldBePositiv)
    {
        if (!shouldBePositiv)
        {
            updateDirectionStamina = -1;
            if (updateStamina == null)
            {
                updateStamina = StartCoroutine(UpdateStamina());
            }
        }
        else
        {
            updateDirectionStamina = 0;
            yield return new WaitForSeconds(waitBeforeGettingBack);
            updateDirectionStamina = 1;
        }
    }

    private IEnumerator UpdateStamina()
    {
        while (CurrentStamina <= maxStamina)
        {
            CurrentStamina += updateDirectionStamina * Time.deltaTime * loseSpeed;
            if (CurrentStamina <= 0 && updateDirectionStamina < 0)
            {
                StopRunning();
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        CurrentStamina = maxStamina;
        updateStamina = null;
    }
}
