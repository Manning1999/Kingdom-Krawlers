using System.Collections;
using System.Collections.Generic;
using ManningsLootSystem;
using UnityEngine;

public class HealthPotion : Potion
{
  

    public override void Use()
    {
        base.Use();

        //give the player health using the TakeDamage method because a double negative makes a positive
        Debug.Log("used a potion");
        PlayerController.Instance.TakeDamage(-(PlayerController.Instance._maxHealth - PlayerController.Instance._currentHealth));
    }
}
