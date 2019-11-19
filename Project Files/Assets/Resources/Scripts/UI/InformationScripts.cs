using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationScripts : MonoBehaviour
{
    public string itemNameText;
    public string descriptionText;
    public string damageText;
    public string speedText;
    public string valueText;
    public Text informationText;

    // Update is called once per frame
    void Update()
    {
        informationText.text = itemNameText + descriptionText + damageText + speedText + valueText;
    }
}
