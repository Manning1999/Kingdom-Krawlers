using System.Collections;
using System.Collections.Generic;
using ManningsLootSystem;
using UnityEngine;

public class InventoryBox : MonoBehaviour
{

    protected GameObject linkedLoot;

    [SerializeField]
    protected GameObject iconDisplay;
    protected UnityEngine.UI.Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = iconDisplay.transform.GetComponent<UnityEngine.UI.Image>();
        SetDetails(null);
    }

    // Update is called once per frame
    void Update()
    {
            
    }


    public void SetDetails(GameObject loot)
    {
        

        if(loot != null)
        {
            image.enabled = true;
            image.sprite = loot.transform.GetComponent<Loot>()._icon;
            linkedLoot = loot;
            Debug.Log(linkedLoot);
        }
        else
        {
            if (image != null)
            {
                image.enabled = false;
            }
        }
    }


    public void Select()
    {

            Debug.Log("Clicked on" + linkedLoot + "   :   " + gameObject.GetInstanceID());
            InventoryController.Instance.SelectLoot(linkedLoot);
       
    }
}
