using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{

    //create singleton
    public static InventoryController instance;
    private static InventoryController _instance;

    public static InventoryController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InventoryController>();
            }

            return _instance;
        }
    }


    [SerializeField]
    protected int inventorySize;

    [SerializeField]
    protected List<GameObject> inventoryItems = new List<GameObject>();

    private InventoryController inventoryInstance;

    [SerializeField]
    protected int playerMoney;



    // Start is called before the first frame update
    void Start()
    {
        if(inventorySize <= 0)
        {
            Debug.Log("INVENTORY SIZE IS " + inventorySize + " Set to a nnumber greater than or equal to 1");
        }   
    }

    private void Awake()
    {
        if (inventoryInstance == null)
        {
            inventoryInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public bool AddToInventory(GameObject loot)
    {

        if (inventoryItems.Count < inventorySize)
        {
            if (!inventoryItems.Contains(loot))
            {
                inventoryItems.Add(loot);
                Debug.Log("Succesfully added");
            }
            return true;

        }
        else
        {
            return false;
        }


    }


    public void SellObject(int moneyRecieved)
    {
        playerMoney += moneyRecieved;
    }

    public void BuyObject(int moneySpent)
    {
        playerMoney -= moneySpent;
    }
}
