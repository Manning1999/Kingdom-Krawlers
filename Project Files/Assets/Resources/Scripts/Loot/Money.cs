using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ManningsLootSystem
{
    public class Money : Loot
    {
       

      
      

        public override void Generate()
        {
            if (LootController.Instance._moneyStat.createMultipleCoins == false)
            {
                value = Random.Range(LootController.Instance._moneyStat.minValue, LootController.Instance._moneyStat.maxValue);
            }
            else
            {
                value = 1;
            }

            
        }

        public override void AddToInventory()
        {

                
            InventoryController.Instance.SellObject(value);
            Destroy(gameObject);
        }
    }
}