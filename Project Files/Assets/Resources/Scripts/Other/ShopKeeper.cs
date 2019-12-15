using System.Collections;
using System.Collections.Generic;
using ManningsLootSystem;
using UnityEngine;

public class ShopKeeper : MonoBehaviour, IInteractable, ICustomDescription
{
    public string description()
    {
        return "Shop Keeper \nSell Items";
    }

    public void InteractWith()
    {
        Debug.Log("Should be able to sell things now"); 
        InventoryController.Instance.ShowInventory(true, true);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
