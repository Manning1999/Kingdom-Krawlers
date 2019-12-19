using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManningsLootSystem
{
    public class Loot : MonoBehaviour
    {

        

        [SerializeField]
        protected Sprite icon = null;

        public Sprite _icon { get { return icon; } }

        [SerializeField]
        protected new string name;
        public string _name { get { return name; } }

        [SerializeField]
        protected int value;
        public int _value { get { return value; } }

        [SerializeField]
        [TextArea(2, 5)]
        protected string description;
        public string _description { get { return description; } }


        protected bool inInventory = false;
        protected bool statsDecided = false;

        [SerializeField]
        protected bool purchasable = false;

        [SerializeField]
        protected bool replenishable = false;


        //Components
        protected SpriteRenderer rend;
        protected Animator anim;


        [SerializeField]
        protected GameObject owner = null;


        // Start is called before the first frame update
        protected virtual void Start()
        {
            if (statsDecided == false)
            {
                Generate();
            }

            try { rend = transform.GetComponent<SpriteRenderer>(); } catch (Exception e) { }
            try { rend.sprite = icon; } catch (Exception e) { }
            try { anim = transform.GetComponent<Animator>(); } catch (Exception e) { }

            
            anim = transform.GetComponent<Animator>();
        }

        protected virtual void Awake()
        {
           
                
            DontDestroyOnLoad(gameObject);

            if(owner == null)
            {
                if (transform.GetComponent<SingletonDontDestroyOnLoad>() == null)
                {
                    gameObject.AddComponent<SingletonDontDestroyOnLoad>();
                    transform.GetComponent<SingletonDontDestroyOnLoad>().GenerateKey();
                }
            }
        }

        // Update is called once per frame
        protected virtual void Update()
        {

            //Destroy all loot if my loot player controller isn't in the scene
            if (ManningsLootSystemPlayerController.Instance == null)
            {
                Debug.Log("Player needs to have the ManningsLootSystemPlayerController attached for loot to work");
                MessageLog.Instance.UpdateLog("Loot Disabled: Attach ManningsLootSystemPlayerCotnroller to player to enable loot");

                Destroy(gameObject);
            }

        }

        public virtual void Generate()
        {
            
        }

         /*
        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (purchasable == false)
            {
                if (inInventory == false)
                {
                    if (col.transform.GetComponent<PlayerController>() != null)
                    {
                        Debug.Log("Should be adding now");
                        AddToInventory();

                    }
                }
            }
        }   */



        public virtual void AddToInventory()
        {
           
                if (InventoryController.Instance.AddToInventory(gameObject) == true)
                {
                    anim.SetTrigger("Collect");
                    inInventory = true;
                }

                owner = PlayerController.Instance.gameObject;
                transform.GetComponent<Collider2D>().enabled = false;
           // Destroy(transform.GetComponent<SingletonDontDestroyOnLoad>());
           
        }



       



        public virtual void Sell()
        {
            
        }







       

        public virtual void InteractWith()
        {
            if(purchasable == true)
            {
                if (InventoryController.Instance._playerMoney >= value)
                {
                    AddToInventory();
                    Debug.Log("purchased: " + transform.name);
                    InventoryController.Instance.BuyObject(value);
                }
                else
                {
                    MessageLog.Instance.UpdateLog("You do not have enough money to buy this");
                    Debug.Log("Couldn't purchase item");
                }
            }
            else
            {
                AddToInventory();
            }
        }
    }

}