using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLoot : MonoBehaviour
{
    public List<GameObject> lights;
    public float[] table =
    {
     65, // Coins (5-15)
     15, // Arrows
     10, // Grey Sword
     5, // Health Potion      
     4, // Grey Bow
     1, //Blue Sword


    };

    public float total;
    public float randomNumber;
    private void Start()
    {
        // tally total weight
        // draw random number between 0 and total weight (100) 

        foreach(var item in table)
        {
            total += item;
        }

        randomNumber = UnityEngine.Random.Range(0, (total));

       for(int i = 0; i < table.Length; i++)
        {
            if (randomNumber <= table[i])
            {
                lights[i].SetActive(true);
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
