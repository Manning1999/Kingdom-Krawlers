using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    public AudioSource buttonSound;

    public void ButtonClick()
    {
        buttonSound.Play(0);
        StartCoroutine(AfterButtonClick());
    }

    public IEnumerator AfterButtonClick()
    {
        yield return new WaitForSeconds(0.1f);
        Application.Quit();
    }
}
