using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManningsLootSystem
{

    public class Potion : Loot, IInteractable
    {

        [SerializeField]
        protected AudioClip useSound;

        protected AudioSource audio;

        protected void Start()
        {
            base.Start();
            audio = transform.GetComponent<AudioSource>();
        }

        public virtual void Use()
        {
            try
            {
                audio.clip = useSound;
                audio.Play();
            }
            catch (Exception e) { }
        }


        public override void AddToInventory()
        {
            InventoryController.Instance.AddPotion(this);
            audio.clip = useSound;
            audio.Play();
            Debug.Log(this);
            if (purchasable == true)
            {
                if(replenishable == false)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }


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
        }

    }



   

}