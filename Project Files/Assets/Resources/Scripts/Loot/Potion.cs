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
        }

        public override void Use()
        {

        }


        public override void AddToInventory()
        {
            InventoryController.Instance.AddPotion(this);
            Debug.Log(this);
            Destroy(gameObject);
        }

    }

}