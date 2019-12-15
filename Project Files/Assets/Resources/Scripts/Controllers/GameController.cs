using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //create singleton
    public static GameController instance;
    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameController>();
            }

            return _instance;
        }
    }




    [SerializeField]
    private List<EnemyIdentifier> enemies = new List<EnemyIdentifier>();

    [SerializeField]
    private List<ChestLootDropBehaviour> chests = new List<ChestLootDropBehaviour>();

    // Start is called before the first frame update
    void Start()
    {
       
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    protected void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        UpdateEnemyList();
        UpdateChestList();

    }
        // Update is called once per frame
        void Update()
    {
        
    }

    private void UpdateEnemyList()
    {
        
        foreach (EnemyIdentifier enemy in GameObject.FindObjectsOfType<EnemyIdentifier>())
        {
            enemies.Add(enemy);
        }
        SetEnemies();

    }

    private void UpdateChestList()
    {
        
        foreach (ChestLootDropBehaviour chest in GameObject.FindObjectsOfType<ChestLootDropBehaviour>())
        {
            chests.Add(chest);
        }
        SetChests();
    }


   private void SetEnemies()
    {
        foreach(EnemyIdentifier enemy in enemies)
        {
            if(enemy._health <= 0)
            {
                enemy.gameObject.SetActive(false);
                Debug.Log("Set Inactive");
            }
            else
            {
                if (enemy._originalScene == SceneManager.GetActiveScene().name)
                {
                    enemy.gameObject.SetActive(true);
                    Debug.Log("Set Active");
                }
                else
                {
                    enemy.gameObject.SetActive(false);
                    Debug.Log("Set Inactive");
                }
            }
        }
    }

    private void SetChests()
    {
        foreach(ChestLootDropBehaviour chest in chests)
        {
            if(chest._originalLevel == SceneManager.GetActiveScene().name)
            {
                chest.gameObject.SetActive(true);
            }
            else
            {
                chest.gameObject.SetActive(false);
            }
        }
    }



    public void ResetEnemies()
    {
        foreach (EnemyIdentifier enemy in enemies)
        {
            enemy.Reset();
        }
    }


    public void ResetChests()
    {
        foreach(ChestLootDropBehaviour chest in chests)
        {
            chest.Reset();
        }
    }
}
