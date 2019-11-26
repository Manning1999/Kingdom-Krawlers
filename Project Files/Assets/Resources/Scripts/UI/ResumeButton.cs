using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject videoUI;
    public GameObject audioUI;

    public void ButtonClick()
    {
        mainMenuUI.SetActive(true);
        videoUI.SetActive(false);
        audioUI.SetActive(false);
    }
}
