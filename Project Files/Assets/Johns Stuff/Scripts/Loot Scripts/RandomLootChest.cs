using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLootChest : MonoBehaviour
{
   

    public List<GameObject> loot;
    public float[] table =
    {
     9, // Coins (5-500)
     9, // Arrows
     9, // Grey Sword
     9, // Health Potion      
     9, // Grey Bow
     9, // Blue Sword
     9, // Blue Bow
     9, // Health Regen Potion
     9, // Speed Potion
     9, // Strength Potion
     9, // Invisibility Potion
     9, // Haste Potion
    };

    public float total;
    public float randomNumber;
    private void Start()
    {
        // tally total weight
        // draw random number between 0 and total weight (100) 

        foreach (var item in table)
        {
            total += item;
             
        }

        randomNumber = UnityEngine.Random.Range(0, (total));

        for (int i = 0; i < table.Length; i++)
        {
            if (randomNumber <= table[i])
            {
                Instantiate(loot[i]);     //Spawns item
                return;
            }
            else
            {
                randomNumber -= table[i];

            }
        }


        //randomnumber = 49

        // is 40 <= 65
        // Coins (5 - 15)

        //randomnumber = 66
        // is 66 <= 65
        //no
        // 66 - 65 = 1
        //1 <= 15
        //Arrows

        //randomnumber = 95
        //is 95 <= 65
        // 95 - 65 = 30
        // 30 <= 15?
        // 30 - 15 = 15
        // 15 <= 10?
        // 15 - 10 = 5
        // 5 <= 5?
        // Health potion
    }
}