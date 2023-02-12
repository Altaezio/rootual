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
    [SerializeField]
    private PlayerMovement mrRacineMovement;

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

        StartCoroutine(CoolDown());
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(attackTime);
        mrRacineMovement.IsImmobilze(false);
        yield return new WaitForSeconds(coolDown - attackTime);
        onCoolDown = false;
        coolDownBack.Play();
    }

    private IEnumerator Emerge()
    {
        float time = 0;
        while (time <= attackTime * .5f)
        {
            killingMouth.transform.localPosition = (killingMouth.transform.localPosition.y + (3.75f * 2 / attackTime) * Time.deltaTime) * Vector3.up;
            time += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        StartCoroutine(BackToDust());
    }

    private IEnumerator BackToDust()
    {
        float time = 0;
        while (time <= attackTime * .5f)
        {
            killingMouth.transform.localPosition = (killingMouth.transform.localPosition.y - (3.75f * 2 / attackTime) * Time.deltaTime) * Vector3.up;
            time += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        killingMouth.SetActive(false);
    }
}
