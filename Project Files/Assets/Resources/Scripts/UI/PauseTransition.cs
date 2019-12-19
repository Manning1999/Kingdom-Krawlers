using System.Collections;
using System.Collections.Generic;
using ManningsLootSystem;
using UnityEngine;
using UnityEngine.UI;

public class PauseTransition : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject hud;

    private bool isPaused = false;
    public bool _isPaused { get { return isPaused; } }


    //create singleton
    public static PauseTransition instance;
    private static PauseTransition _instance;

    public static PauseTransition Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<PauseTransition>();
            }

            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Pause(isPaused);
        }
        Debug.Log(Time.timeScale);
    }


    public void Pause(bool set)
    {
        if (set == true)
        {
            hud.SetActive(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            Debug.Log(Time.timeScale);
            InventoryController.Instance.ShowInventory(false);
        }

        if (set == false)
        {
            hud.SetActive(true);
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Debug.Log(Time.timeScale);
            InventoryController.Instance.ShowInventory(false);
        }
    }
}
