using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCountUI : MonoBehaviour
{

    //create singleton
    public static ArrowCountUI instance;
    private static ArrowCountUI _instance;

    public static ArrowCountUI Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ArrowCountUI>();
            }

            return _instance;
        }
    }


    public void UpdateArrowGUI(int arrows)
    {
        transform.GetComponent<Text>().text = arrows.ToString();
    }
}
