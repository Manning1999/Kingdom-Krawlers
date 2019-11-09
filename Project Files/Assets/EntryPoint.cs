using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    //create singleton
    public static EntryPoint instance;
    private static EntryPoint _instance;

    public static EntryPoint Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<EntryPoint>();
            }

            return _instance;
        }
    }


    [SerializeField]
    protected string linkedScene = "";

    public string _linkedScene { get { return linkedScene; } protected set { linkedScene = value; } }
}
