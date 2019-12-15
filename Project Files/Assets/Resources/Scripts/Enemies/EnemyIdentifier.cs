using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyIdentifier : MonoBehaviour
{
    protected int baseHealth = 0;

    [SerializeField]
    protected int health;

    public int _health { get { return health; } }

    
    protected string originalScene = "";
    public string _originalScene { get { return originalScene; } }


    protected virtual void Start()
    {
        if(originalScene.Equals(""))
        {
            Debug.Log("Setting original scene");
            originalScene = SceneManager.GetActiveScene().name;
        }
        if (baseHealth == 0)
        {
            baseHealth = health;
        }
    }

    public void Reset()
    {
        health = baseHealth; 
    }

}
