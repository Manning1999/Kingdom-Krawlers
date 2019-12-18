using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenuButton : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject videoUI;
    public GameObject audioUI;
    public AudioSource buttonSound;

    public void ButtonClick()
    {
        buttonSound.Play(0);
        StartCoroutine(AfterButtonClick());
    }

    public IEnumerator AfterButtonClick()
    {
        yield return new WaitForSeconds(0.1f);
        mainMenuUI.SetActive(true);
        videoUI.SetActive(false);
        audioUI.SetActive(false);

    }
}
