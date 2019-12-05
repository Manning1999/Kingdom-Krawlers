using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class goToMainmenu : MonoBehaviour
{
    public void ButtonClick()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
