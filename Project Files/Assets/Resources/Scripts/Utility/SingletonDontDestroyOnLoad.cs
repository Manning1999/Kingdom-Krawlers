using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonDontDestroyOnLoad : MonoBehaviour
{

    [SerializeField]
    [Tooltip("This is the key used to uniquely identify this object. When a new scene loads the object will destroy everything else that has the same key to ensure that only one instance of the object is in the scene at a same")]
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

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

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
                if (instance._originalScene == "")
                {
                    Destroy(instance.gameObject);
                    Debug.Log("Destroying a " + key);
                }
            }
        }

        instanceList.Clear();

        originalScene = SceneManager.GetActiveScene().name;
    }
}
