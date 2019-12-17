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
        buttonSound.Play(0);
        mainMenuUI.SetActive(false);
        videoUI.SetActive(true);
        
    }
}
