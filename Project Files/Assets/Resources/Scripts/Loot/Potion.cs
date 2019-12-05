using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManningsLootSystem
{

    public class Potion : Loot
    {

        [SerializeField]
        protected AudioClip useSound;

        protected AudioSource audio;

        protected void Start()
        {
            base.Start();
            audio = transform.GetComponent<AudioSource>();
        }

        public override void Use()
        {
            audio.clip = useSound;
            audio.Play();
        }


        public override void AddToInventory()
        {
            InventoryController.Instance.AddPotion(this);
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

    }

}