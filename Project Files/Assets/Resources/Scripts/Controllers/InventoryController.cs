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
        private Potion potionTest = null;

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



        [System.Serializable]
        public class Potions
        {
            public Potion potionType = null;
            public int quantityOwned = 0;
            public KeyCode usePotionButton;

            
        }

        [SerializeField]
        protected List<Potions> potions = new List<Potions>();


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



            foreach (Potions pot in potions)
            {
                if (pot.potionType == null)
                {
                    potions.Remove(pot);
                    break;
                }

            }
        }

        // Update is called once per frame
        void Update()
        {
            foreach(Potions pot in potions)
            {
                if (Input.GetKeyDown(pot.usePotionButton))
                {
                    
                    if (pot.quantityOwned > 0)
                    {
                        Debug.Log("Button pressed");
                        pot.potionType.Use();
                        pot.quantityOwned -= 1;
                    }
                }
            }




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


        public void AddPotion(Potion pot)
        {
            audio.clip = collectLootSound;
            audio.Play();

            foreach (Potions potion in potions)
            {
                Debug.Log(potion.potionType + "    :    " + pot);
                if (pot.ToString() == potion.potionType.ToString())
                {
                    Debug.Log(pot);
                    potion.quantityOwned++;
                    break;
                }
            }
        }
    }
}