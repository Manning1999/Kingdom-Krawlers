using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private List<PotentialLootDrop> potentialLootDropList = new List<PotentialLootDrop>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }



    public void DecideLootToDrop()
    {


        int chance = Random.Range(0, 100);

        int dropChanceSoFar = 0;

        foreach(PotentialLootDrop lootDrop in potentialLootDropList)
        {
            if (chance < dropChanceSoFar + lootDrop.dropChance && chance > dropChanceSoFar)
            {
                DropLoot(lootDrop.loot);
                break;
            }
            dropChanceSoFar += lootDrop.dropChance;


        }


    }

    private void DropLoot(Loot lootToDrop)
    {
        Instantiate(lootToDrop, transform.position, Quaternion.identity);
    }
}
