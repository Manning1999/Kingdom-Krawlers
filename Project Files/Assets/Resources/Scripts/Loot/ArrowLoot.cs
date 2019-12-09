using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ManningsLootSystem
{

    public class ArrowLoot : Loot
    {
        [SerializeField]
        protected int arrows = 0;

        public override void Generate()
        {
            if (arrows == 0)
            {
                arrows = Random.Range(LootController.Instance._ArrowStat.minArrowsInDrop, LootController.Instance._ArrowStat.maxArrowsInDrop);
            }
        }

        public override void AddToInventory()
        {
            InventoryController.Instance.UpdateArrows(arrows);
            Destroy(gameObject);
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
