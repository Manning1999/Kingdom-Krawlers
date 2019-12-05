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
        Application.Quit();
    }
}
