using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    public GameObject pauseMenu;

    public void ButtonClick()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
