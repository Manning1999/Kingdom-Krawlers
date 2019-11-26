using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ManningsLootSystem
{
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

        [SerializeField]
        protected int arrowCount;
        public int _arrowCount { get { return arrowCount; } }

        [SerializeField]
        protected AudioClip collectLootSound;

        protected AudioSource audio;



        // Start is called before the first frame update
        void Start()
        {
            if (inventorySize <= 0)
            {
                Debug.Log("INVENTORY SIZE IS " + inventorySize + " Set to a number greater than or equal to 1");
                MessageLog.Instance.UpdateLog("Inventory size is 0. Increase it to a number greater than 1 for inventory to work");
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

            audio = transform.GetComponent<AudioSource>();

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
                audio.clip = collectLootSound;
                audio.Play();
                return true;

            }
            else
            {
                return false;
            }
        }

        public void RemoveFromInventory(GameObject loot)
        {
            inventoryItems.Remove(loot);
        }


        public void SellObject(int moneyRecieved)
        {
            audio.clip = collectLootSound;
            audio.Play();
            
            playerMoney += moneyRecieved;
            GoldCountUI.Instance.UpdateGoldCount(playerMoney);
        }

        public void BuyObject(int moneySpent)
        {
            playerMoney -= moneySpent;
        }


        public void UpdateArrows(int arrowChange)
        {
            arrowCount += arrowChange;
            ArrowCountUI.Instance.UpdateArrowGUI(arrowCount);
        }
    }
}