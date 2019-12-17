using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject videoUI;
    public GameObject audioUI;
    public AudioSource buttonSound;

    public void ButtonClick()
    {

        buttonSound.Play(0);
        mainMenuUI.SetActive(true);
        videoUI.SetActive(false);
        audioUI.SetActive(false);
    }
}
