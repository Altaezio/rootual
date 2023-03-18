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
                playerMovement.IsImmobilize(true);
                mrPropreAnim.DeathAnim();
                StartCoroutine(GameOver());
            }
        }
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("RootWin");
    }
}