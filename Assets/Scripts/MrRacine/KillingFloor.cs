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
    private AudioSource attack, missedAttack, coolDownBack, coolDownNotBack;
    [SerializeField]
    private KillingTest testIfAtRange;
    [SerializeField]
    private PlayerMovement mrRacineMovement;
    [SerializeField]
    private AnimationCurve attackMovementCurve;

    private bool onCoolDown;
    private bool IsAtRange { get => testIfAtRange.PlayerInRange; }

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
        mrRacineMovement.IsImmobilze(true);
        if (IsAtRange)
            attack.Play();
        else
            missedAttack.Play();

        StartCoroutine(Emerge());

    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDown);
        onCoolDown = false;
        coolDownBack.Play();
    }

    private IEnumerator Emerge()
    {
        float abscisse = 0;
        while (abscisse <= 1)
        {
            killingMouth.transform.localPosition = attackMovementCurve.Evaluate(abscisse) * Vector3.up;
            abscisse += Time.deltaTime / attackTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        killingMouth.SetActive(false);
        mrRacineMovement.IsImmobilze(false);
        StartCoroutine(CoolDown());
    }
}
