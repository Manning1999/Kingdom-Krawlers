using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityController : MonoBehaviour
{


    [SerializeField]
    private TMPro.TMP_Dropdown resolutionDropDown;

    private bool isFullScreen = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetOverallQuality(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel);     
    }


    public void SetAntiAliasing(int quality)
    {
        QualitySettings.antiAliasing = quality;
    }



    public void SetVsync(int quality)
    {
        QualitySettings.vSyncCount = quality;
    }



    public void SetResolution(int chosenOption)
    {
        //get the title of the selected resolution
        string resolution = resolutionDropDown.options[chosenOption].text;

        
              //break up the resolution title into substrings which can then be parsed in an integer for heigth and width
        int height = int.Parse(resolution.Substring(0, resolution.Length - resolution.IndexOf("×") - 1));
        int width = int.Parse(resolution.Substring(resolution.IndexOf("×") + 1, resolution.Length - resolution.IndexOf("×") - 1));

        //set the screen resolution and make it fullscreen
         Screen.SetResolution(width, height, isFullScreen);
    }



    public void SetFullScreen(bool set)
    {
        isFullScreen = set;
        Screen.SetResolution(Screen.width, Screen.height, isFullScreen);
    }

}
