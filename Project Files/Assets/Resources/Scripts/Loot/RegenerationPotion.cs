using System.Collections;
using System.Collections.Generic;
using ManningsLootSystem;
using UnityEngine;

public class RegenerationPotion : Potion
{
    [SerializeField]
    [Tooltip("How much the potions should heal per second")]
    private int regenerationSpeed = 3;

    [SerializeField]
    [Tooltip("How many seconds the potions should heal the player for")]
    private int regenerationTime = 10;



   

    public override void Use()
    {
        base.Use();

        StartCoroutine(RegenerationTimer());
    }

    private IEnumerator RegenerationTimer()
    {
        for(int i = 0; i < regenerationTime; i++)
        {
            yield return new WaitForSeconds(1);
            PlayerController.Instance.TakeDamage(-regenerationSpeed);
        }
    }


}
