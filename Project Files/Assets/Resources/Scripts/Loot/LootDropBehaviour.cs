using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManningsLootSystem
{
    public class LootDropBehaviour : MonoBehaviour
    {

        [System.Serializable]
        public class PotentialLootDrop
        {
            public Loot loot;
            public int dropChance;
        }

        [SerializeField]
        [Tooltip("Enter what loot you want this object to be able to drop and the chance of it dropping that loot")]
        protected List<PotentialLootDrop> potentialLootDropList = new List<PotentialLootDrop>();


        protected int totalPercentage;

        // Start is called before the first frame update
        protected virtual void Start()
        {

            foreach(PotentialLootDrop drop in potentialLootDropList)
            {
                totalPercentage += drop.dropChance;
            }
        }

        // Update is called once per frame
        protected virtual void Update()
        {

        }



        public virtual void DecideLootToDrop()
        {


            int chance = Random.Range(0, totalPercentage);

            int dropChanceSoFar = 0;

            foreach (PotentialLootDrop lootDrop in potentialLootDropList)
            {
                if (chance < dropChanceSoFar + lootDrop.dropChance && chance > dropChanceSoFar)
                {
                    DropLoot(lootDrop.loot);
                    break;
                }
                dropChanceSoFar += lootDrop.dropChance;


            }


        }

        protected virtual void DropLoot(Loot lootToDrop)
        {
            Debug.Log("Created a " + lootToDrop);
            Debug.Log(transform.position);
            Instantiate(lootToDrop, transform.position, Quaternion.identity);
        }
    }
}