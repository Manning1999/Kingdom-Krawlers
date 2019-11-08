using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    //create singleton
    public static RespawnPoint instance;
    private static RespawnPoint _instance;

    public static RespawnPoint Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<RespawnPoint>();
            }

            return _instance;
        }
    }

}
