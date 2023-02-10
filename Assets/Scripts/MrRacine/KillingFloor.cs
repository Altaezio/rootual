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
    [SerializeField]
    private float attackTime;
    [SerializeField]
    private AudioSource attack;
    [SerializeField]
    private AudioSource missedAttack;
    [SerializeField]
    private AudioSource coolDownBack;
    [SerializeField]
    private AudioSource coolDownNotBack;
    [SerializeField]
    private KillingTest testIfAtRange;
    [SerializeField] PlayerMovement mrRacineMovement;

    private bool onCoolDown;
    private bool isAtRange { get => testIfAtRange.PlayerInRange; }

    private void Start()
    {
        killingMouth.SetActive(false);
        onCoolDown = false;
    }

    public void Eat(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (onCoolDown)
        {
            coolDownNotBack.Play();
            return;
        }
        TryToEat();
    }

    private void TryToEat()
    {
        onCoolDown = true;
        killingMouth.SetActive(true);
        mrRacineMovement.IsImmobilized(true);
        if (isAtRange)
            attack.Play();
        else
            missedAttack.Play();

        StartCoroutine(CoolDown());
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(attackTime);
        killingMouth.SetActive(false);
        mrRacineMovement.IsImmobilized(false);
        yield return new WaitForSeconds(coolDown - attackTime);
        onCoolDown = false;
        coolDownBack.Play();
    }
}
