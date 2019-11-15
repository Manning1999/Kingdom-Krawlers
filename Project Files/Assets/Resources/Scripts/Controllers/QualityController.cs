using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityController : MonoBehaviour
{


    [SerializeField]
    private TMPro.TMP_Dropdown resolutionDropDown;

    private bool isFullScreen = true;

    [SerializeField]
    private List<UnityEngine.UI.Button> overallQualityButtons;

    [SerializeField]
    private List<UnityEngine.UI.Button> antiAliasingButtons;

    [SerializeField]
    private List<UnityEngine.UI.Button> vSyncButtons;

    [SerializeField]
    private List<UnityEngine.UI.Button> textureQualityButtons;

    


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
        
        foreach(UnityEngine.UI.Button button in antiAliasingButtons)
        {
            if (button.transform.GetComponent<QualitySetting>()._settingValue == QualitySettings.antiAliasing)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }

        foreach (UnityEngine.UI.Button button in vSyncButtons)
        {
            if(button.transform.GetComponent<QualitySetting>()._settingValue == QualitySettings.vSyncCount)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }

        foreach (UnityEngine.UI.Button button in textureQualityButtons)
        {
            if (button.transform.GetComponent<QualitySetting>()._settingValue == QualitySettings.masterTextureLimit)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }


    }


    public void SetAntiAliasing(int quality)
    {
        Debug.Log(QualitySettings.antiAliasing);
        QualitySettings.antiAliasing = quality;

        foreach(UnityEngine.UI.Button button in overallQualityButtons)
        {
            button.interactable = true;
        }

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

    public void SetTextureQuality(int quality)
    {
        QualitySettings.masterTextureLimit = quality;
    }

}
