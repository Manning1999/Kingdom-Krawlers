using System;
using System.Collections;
using System.Collections.Generic;
using ManningsLootSystem;
using UnityEngine;

public class InventoryBox : MonoBehaviour
{

    protected GameObject linkedLoot;
    public GameObject _linkedLoot { get { return linkedLoot; } }

    [SerializeField]
    protected GameObject iconDisplay;
    protected UnityEngine.UI.Image image;

    [SerializeField]
    protected Color selectedColor;

    // Start is called before the first frame update
    void Awake()
    {
        if (image == null)
        {
            image = iconDisplay.transform.GetComponent<UnityEngine.UI.Image>();
        }
       // SetDetails(null);
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    private void OnEnable()
    {
       
        if(linkedLoot != null)
        {
            
            image.enabled = true;
            Debug.Log(image.IsActive());
            image.sprite = linkedLoot.transform.GetComponent<Loot>()._icon;
            Debug.Log(linkedLoot.transform.GetComponent<Loot>()._icon);
        }
        else
        {
            image.enabled = false;
        }
    }


    public void SetDetails(GameObject loot)
    {
        

        if(loot != null)
        {
           
            linkedLoot = loot;
            try
            {
                image.enabled = true;
                image.sprite = linkedLoot.transform.GetComponent<Loot>()._icon;
            }
            catch (Exception e) { }
        }
        else
        {
            linkedLoot = null;


            if (image != null)
            {
                try
                {
                    image.enabled = false;
                    
                }
                catch (Exception e) { }
            }
        }
    }


     
    public void Select(bool selected)
    {

        if (selected == true)
        {
            InventoryController.Instance.SelectLoot(linkedLoot);

            if (linkedLoot != null)
            {
          
               
                transform.GetComponent<UnityEngine.UI.Image>().color = selectedColor;
            }
        }
        else
        {
            transform.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
       
    }
}
