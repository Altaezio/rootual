using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManagment : MonoBehaviour
{
    [SerializeField] private int lives;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private MrPropreAnim mrPropreAnim;
    [SerializeField] private Rigidbody rb;

    public int Lives
    {
        get => lives;
        set
        {
            lives = value;
            if (lives == 0)
            {
                playerMovement.IsImmobilze(true);
                // rb.constraints = RigidbodyConstraints.None;
                // transform.Rotate(0, 0, 5);
                mrPropreAnim.DeathAnim();
                StartCoroutine(GoToNextScene());
            }
        }
    }

    private IEnumerator GoToNextScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("RootWin"); // SceneManager.GetActiveScene().buildIndex + 1
    }
}