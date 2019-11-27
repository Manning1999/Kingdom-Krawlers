using System.Collections;
using System.Collections.Generic;
using ManningsLootSystem;
using UnityEngine;

public class HealthPotion : Potion
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use()
    {
        //give the player health using the TakeDamage method because a double negative makes a positive
        Debug.Log("used a potion");
        PlayerController.Instance.TakeDamage(-(PlayerController.Instance._maxHealth - PlayerController.Instance._currentHealth));
    }
}
