using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadBody : MonoBehaviour
{


    protected string levelDiedOn = "";

    [SerializeField]
    protected GameObject soulObject;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (levelDiedOn == "")
        {
            levelDiedOn = SceneManager.GetActiveScene().name;
            Debug.Log(levelDiedOn);
        }

    }

    protected void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        

        if(SceneManager.GetActiveScene().name == levelDiedOn)
        {
            transform.GetComponent<SpriteRenderer>().enabled = true;
            transform.GetComponent<Collider2D>().enabled = true;
            soulObject.SetActive(true);
            Debug.Log("Enabled dead body");
        }
        else
        {
            Debug.Log("Disabled dead body");
            transform.GetComponent<SpriteRenderer>().enabled = false;
            transform.GetComponent<Collider2D>().enabled = false;
            soulObject.SetActive(false);
        }
    }

    public void Hide()
    {
        transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetComponent<Collider2D>().enabled = false;
        soulObject.SetActive(false);
    }

    
}
