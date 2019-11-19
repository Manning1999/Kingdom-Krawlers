using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioButton : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject audioUI;

    public void ButtonClick()
    {
        mainMenuUI.SetActive(false);
        audioUI.SetActive(true);
    }
}
