using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//The inventory assume there will be four boxes per row and I can't be bothered to do the maths to make it dynamic

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



        [System.Serializable]
        public class Potions
        {
            public Potion potionType = null;
            public int quantityOwned = 0;
            public KeyCode usePotionButton;
            public TextMeshProUGUI inventoryText;

        }

        [SerializeField]
        protected List<Potions> potions = new List<Potions>();


        [Tooltip("Inventory Menu")]

        [SerializeField]
        protected GameObject inventoryObject = null;
        [SerializeField]
        protected GameObject inventoryBox = null;

        [SerializeField]
        protected GameObject selectedLoot = null;

        [SerializeField]
        protected GameObject scrollViewContent = null;


        protected List<GameObject> boxes = new List<GameObject>();

        [SerializeField]
        protected TextMeshProUGUI infoBox = null;


        protected AudioSource audio;

        protected bool showingInventory = false;





        // Start is called before the first frame update
        void Start()
        {
            if (inventorySize <= 0)
            {
                Debug.Log("INVENTORY SIZE IS " + inventorySize + " Set to a number greater than or equal to 1");
                MessageLog.Instance.UpdateLog("Inventory size is 0. Increase it to a number greater than 1 for inventory to work");
            }

            PopulateInventoryTiles();
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
            foreach (Potions pot in potions)
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


            if (Input.GetKeyDown("i"))
            {
                ShowInventory(!showingInventory);
            }




        }


        public bool AddToInventory(GameObject loot)
        {



            if (inventoryItems.Count < inventorySize)
            {
                if (!inventoryItems.Contains(loot))
                {
                    inventoryItems.Add(loot);
            
                }
                audio.clip = collectLootSound;
                audio.Play();
                PopulateInventory();
                return true;


            }
            else
            {
                return false;
            }
        }

        public void RemoveFromInventory()
        {
            if (selectedLoot != null)
            {
                if (selectedLoot.transform.GetComponent<Bow>() != null)
                {
                    
                    if (ManningsLootSystemPlayerController.Instance._equippedBow == selectedLoot)
                    {
                        // selectedLoot.transform.GetComponent<Bow>().Equip(false);
                        ManningsLootSystemPlayerController.Instance.EquipBow(null);
                    }
                }
                if (selectedLoot.transform.GetComponent<Sword>() != null)
                {
                    if (ManningsLootSystemPlayerController.Instance._equippedSword == selectedLoot)
                    {
                        // selectedLoot.transform.GetComponent<Sword>().Equip(false);
                        ManningsLootSystemPlayerController.Instance.EquipSword(null);
                    }
                }
                Debug.Log("Pressed remove button");
                inventoryItems.Remove(selectedLoot);
                PopulateInventory();
                ShowInventory(false);
            }
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


        protected void PopulateInventoryTiles()
        {


            for (int i = 0; i < inventorySize; i++)
            {
                GameObject newBox = Instantiate(inventoryBox, scrollViewContent.transform);
                boxes.Add(newBox);
                
            }
            PopulateInventory();
        }

        [ContextMenu("Show loot collected")]
        public void PopulateInventory()
        {
            /*for (int i = 0; i < inventoryItems.Count; i++)
            {
                boxes[i].transform.GetComponent<InventoryBox>().SetDetails(inventoryItems[i]);
            } */
            for(int i = 0; i<inventorySize; i++)
            {
                if(i < inventoryItems.Count) {
                    
                        boxes[i].transform.GetComponent<InventoryBox>().SetDetails(inventoryItems[i]);
                    
                }
                else
                {
                    boxes[i].transform.GetComponent<InventoryBox>().SetDetails(null);
                }
            }
        }


        public void SelectLoot(GameObject loot)
        {
            if(selectedLoot != null)
            {
                foreach(GameObject box in boxes)
                {
                    if(box.transform.GetComponent<InventoryBox>()._linkedLoot == selectedLoot)
                    {
                        box.transform.GetComponent<InventoryBox>().Select(false);
                        break;
                    }
                }
            }


            selectedLoot = loot;
            Debug.Log(loot);
            infoBox.text = loot.name + "\n\n" + loot.transform.GetComponent<Loot>()._description + "\n\nValue: " + loot.transform.GetComponent<Loot>()._value;
        }



        public void Equip()
        {
            if (selectedLoot != null)
            {
                /* if (selectedLoot.transform.GetComponent<Sword>() != null)
                 {
                    // ManningsLootSystemPlayerController.Instance.EquipSword(selectedLoot.transform.GetComponent<Sword>());
                 }
                 else if (selectedLoot.transform.GetComponent<Bow>() != null)
                 {
                     ManningsLootSystemPlayerController.Instance.EquipBow(selectedLoot.transform.GetComponent<Bow>());
                 }  */
                if (selectedLoot.transform.GetComponent<Sword>() != null)
                {
                    //  selectedLoot.transform.GetComponent<Sword>().Equip(true);
                    ManningsLootSystemPlayerController.Instance.EquipSword(selectedLoot.transform.GetComponent<Sword>());
                }
                else if (selectedLoot.transform.GetComponent<Bow>() != null)
                {
                    ManningsLootSystemPlayerController.Instance.EquipBow(selectedLoot.transform.GetComponent<Bow>());
                }
                


            }
        }


        public void ShowInventory(bool show)
        {
            if(show == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }


            showingInventory = !showingInventory;
            inventoryObject.SetActive(show);

            foreach(Potions pot in potions)
            {
                pot.inventoryText.text = " X " + pot.quantityOwned;
            }
        }
    }
}