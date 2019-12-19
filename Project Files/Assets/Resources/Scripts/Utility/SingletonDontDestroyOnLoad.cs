using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class SingletonDontDestroyOnLoad : MonoBehaviour
{

    [SerializeField]
    [Tooltip("This is the key used to uniquely identify this object. When a new scene loads the object will destroy everything else that has the same key to ensure that only one instance of the object is in the scene at a same. If this is left empty, the key will be set to be the gameObject's position")]
    private string key;

    public string _key { get { return key; } }

    [SerializeField]
    private string originalScene;
    public string _originalScene { get {return originalScene; } }



     [SerializeField]
    private List<SingletonDontDestroyOnLoad> instanceList = new List<SingletonDontDestroyOnLoad>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        
        

    }



    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        GenerateKey();

    }


    public void GenerateKey() {
        try
        {
            if (key == "")
            {

                key = transform.position.ToString();

            }
        }
        catch(Exception e)
        {
            Debug.Log(transform.name);
        }

        
        instanceList.Clear();

       

        foreach (SingletonDontDestroyOnLoad instance in FindObjectsOfType<SingletonDontDestroyOnLoad>())
        {

            if (instance._key == key)
            {
                instanceList.Add(instance);
            }

        }

        

        if(instanceList.Count == 1)
        {
            if (originalScene == "")
            {
                originalScene = SceneManager.GetActiveScene().name;
            }
        }
        else
        {
            foreach (SingletonDontDestroyOnLoad instance in instanceList)
            {
                if (instance._originalScene == "" && instanceList.Count > 1)
                {
                    Destroy(instance.gameObject);
                    Debug.Log("Destroying");
                }
            }
        }

        instanceList.Clear();

        originalScene = SceneManager.GetActiveScene().name;
    }
}
