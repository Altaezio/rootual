using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManagment : MonoBehaviour
{
    [SerializeField]
    private int lives;

    public int Lives
    {
        get => lives;
        set
        {
            lives = value;
            if (lives == 0)
            {
                StartCoroutine(GoToNextScene());
            }
        }
    }

    private IEnumerator GoToNextScene()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Next scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}