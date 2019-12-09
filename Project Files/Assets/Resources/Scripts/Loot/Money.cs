using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ManningsLootSystem
{
    public class Money : Loot
    {


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