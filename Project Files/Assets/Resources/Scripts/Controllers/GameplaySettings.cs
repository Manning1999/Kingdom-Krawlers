using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySettings : MonoBehaviour
{


    //create singleton
    public static GameplaySettings instance;
    private static GameplaySettings _instance;

    public static GameplaySettings Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameplaySettings>();
            }

            return _instance;
        }
    }


    private bool dashTowardsMousePosition = true;
    public bool _dashTowardsMousePosition { get { return dashTowardsMousePosition; } private set { dashTowardsMousePosition = value; } }


   


    // Start is called before the first frame update
    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDashType(bool type)
    {
        dashTowardsMousePosition = type;

        try
        {
            PlayerController.Instance.ChangeDashType(type);
        }
        catch (Exception e) { }
    }
}
