using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoButton : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject videoUI;
    public AudioSource buttonSound;


    public void ButtonClick()
    {
        StartCoroutine(AfterButtonClick());
        buttonSound.Play(0);
    }

    public IEnumerator AfterButtonClick()
    {
        yield return new WaitForSeconds(0.1f);
        mainMenuUI.SetActive(false);
        videoUI.SetActive(true);
    }
}
