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

  

        [SerializeField]
        protected int playerMoney;

        public int _playerMoney { get { return playerMoney; } }

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
        [Tooltip("This should be a prefab that has the InventoryBox script attached")]
        protected GameObject inventoryBox = null;

        [SerializeField]
        protected GameObject sellButton = null;

        [SerializeField]
        protected GameObject selectedLoot = null;

        [SerializeField]
        protected GameObject scrollViewContent = null;


        protected List<GameObject> boxes = new List<GameObject>();

        [SerializeField]
        protected TextMeshProUGUI infoBox = null;


        protected new AudioSource audio;

        protected bool showingInventory = false;

        protected bool canSell = false;
        public bool _canSell { get { return canSell; } }



        // Start is called before the first frame update
        void Start()
        {
            if (inventorySize <= 0)
            {
                Debug.Log("INVENTORY SIZE IS " + inventorySize + " Set to a number greater than or equal to 1");
                MessageLog.Instance.UpdateLog("Inventory size is 0. Increase it to a number greater than 1 for inventory to work");
            }

            PopulateInventoryTiles();
            GoldCountUI.Instance.UpdateGoldCount(playerMoney);
        }

        private void Awake()
        {
           

            audio = transform.GetComponent<AudioSource>();

       



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
                
                Destroy(selectedLoot.gameObject);
            }
        }


        public void SellObject(int moneyRecieved)
        {
            audio.clip = collectLootSound;
            audio.Play();

            playerMoney += moneyRecieved;
            GoldCountUI.Instance.UpdateGoldCount(playerMoney);
        }


        public void SellButton()
        {
            if(selectedLoot != null)
            {
                SellObject(selectedLoot.transform.GetComponent<Loot>()._value);
                inventoryItems.Remove(selectedLoot);
                PopulateInventory();
                Destroy(selectedLoot.gameObject);
            }
        }

        public void BuyObject(int moneySpent)
        {
           
            playerMoney -= moneySpent;
            GoldCountUI.Instance.UpdateGoldCount(playerMoney);
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

        //Create all the inventory tiles
        protected void PopulateInventoryTiles()
        {


            for (int i = 0; i < inventorySize; i++)
            {
                GameObject newBox = Instantiate(inventoryBox, scrollViewContent.transform);
                boxes.Add(newBox);
                
            }
            PopulateInventory();
        }


        //Display all the inventory items int he inventory tiles
        [ContextMenu("Show loot collected")]
        public void PopulateInventory()
        {
           
            for(int i = 0; i<inventorySize; i++)
            {
                if(i < inventoryItems.Count) {
                    
                    boxes[i].transform.GetComponent<InventoryBox>().SetDetails(inventoryItems[i]);
                    boxes[i].transform.GetComponent<InventoryBox>().Select(false);


                }
                else
                {
                    boxes[i].transform.GetComponent<InventoryBox>().SetDetails(null);
                    
                }
            }

            infoBox.text = "";
        }

        
        public void SelectLoot(GameObject loot)
        {
            if (loot != null)
            {
                if (selectedLoot != null)
                {
                    foreach (GameObject box in boxes)
                    {
                        if (box.transform.GetComponent<InventoryBox>()._linkedLoot == selectedLoot)
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
        }



        public void Equip()
        {
            if (selectedLoot != null)
            {
               
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


        public void ShowInventory(bool show, bool showCanSellButton = false)
        {
            if(show == true)
            {
                Time.timeScale = 0;
               
            }
            else
            {
                Time.timeScale = 1;
            }

            canSell = showCanSellButton;
            sellButton.SetActive(canSell);


            showingInventory = !showingInventory;
            inventoryObject.SetActive(show);
            PopulateInventory();
            foreach(Potions pot in potions)
            {
                pot.inventoryText.text = " X " + pot.quantityOwned;
            }
        }
    }
}