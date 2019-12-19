using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class goToMainmenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public AudioSource buttonSound;

    public void ButtonClick()
    {
        StartCoroutine(AfterButtonClick());
        buttonSound.Play(0);
        SceneManager.LoadScene("Main Menu");

    }

    public IEnumerator AfterButtonClick()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Main Menu");
    }
}
