using System;
using System.Collections;
using System.Collections.Generic;
using ManningsLootSystem;
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

   
    //The key in this dictionary is the key from a piece of loot's SingletonDontDestroyOnLoad key
    private Dictionary<string, Loot> lootDictionary = new Dictionary<string, Loot>();



    // Start is called before the first frame update
    void Awake()
    {
       
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    protected void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

      //  UpdateEnemyList();
        UpdateChestList();
        UpdateLootList();
    }


    // Update is called once per frame
    void Update()
    {
        
    }



     
    private void UpdateLootList()
    {
        foreach (Loot loot in FindObjectsOfType<Loot>())
        {
            if (loot.transform.GetComponent<SingletonDontDestroyOnLoad>() != null)
            {

                if (!lootDictionary.ContainsKey(loot.transform.GetComponent<SingletonDontDestroyOnLoad>()._key))
                {
                    lootDictionary.Add(loot.transform.GetComponent<SingletonDontDestroyOnLoad>()._key, loot);
                }
               /* else
                {
                    Destroy(loot.gameObject);
                } */ 
            }
        }
    } 
    

    private void SetLoot()
    {
        foreach(Loot loot in FindObjectsOfType<Loot>())
        {
            if (lootDictionary.ContainsKey(loot.transform.GetComponent<SingletonDontDestroyOnLoad>()._key))
            {
                Loot currentLoot = lootDictionary[loot.transform.GetComponent<SingletonDontDestroyOnLoad>()._key];

               

                if (SceneManager.GetActiveScene().name == currentLoot.transform.GetComponent<SingletonDontDestroyOnLoad>()._originalScene)
                {
                    currentLoot.gameObject.SetActive(true);
                }
                else
                {
                    currentLoot.gameObject.SetActive(false);
                }

            }
        }
        SetLoot();
    }
        


    //Not in use any more. It caused a major bug that would destroy all the enemies if you started in a scene that didn't contain them.
    private void UpdateEnemyList()
    {
         //Get every enemy in the scene
        foreach (EnemyIdentifier enemy in GameObject.FindObjectsOfType<EnemyIdentifier>())
        {
            try
            {
                bool canAdd = true;
                if (!enemies.Contains(enemy))
                {
                     //get every enemy already in the list of enemies
                    foreach (EnemyIdentifier enem in enemies)
                    {
                        //if the enemy is already in the list then destroy it
                        if (enem.transform.GetComponent<SingletonDontDestroyOnLoad>()._key == enemy.transform.GetComponent<SingletonDontDestroyOnLoad>()._key)
                        {
                            enemies.Remove(enemy);
                            Destroy(enemy.gameObject);
                            Debug.Log("Controller destroying");
                            canAdd = false;
                            break;
                        }
                    }
                    if (canAdd == true) enemies.Add(enemy);

                }
            }
            catch(Exception e)
            {
                continue;
            }
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


    //Not in use any more. It caused a major bug that would destroy all the enemies if you started in a scene that didn't contain them.
    private void SetEnemies()
    {
        foreach(EnemyIdentifier enemy in enemies)
        {
            try
            {
                if (!enemy._originalScene.Equals(""))
                {
                    if (enemy._health <= 0)
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
            catch (Exception e) {
                continue;
            }
        }
    }

    private void SetChests()
    {
       
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
