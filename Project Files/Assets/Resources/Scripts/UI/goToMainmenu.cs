using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class goToMainmenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public void ButtonClick()
    {
        pauseMenu.SetActive(false);
        SceneManager.LoadScene("Main Menu");
    }
}
