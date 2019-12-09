using System.Collections;
using System.Collections.Generic;
using ManningsLootSystem;
using UnityEngine;

public class KingdomKrawlerInteractor : Interactor
{


    protected override void ShowText()
    {
        if (objectToInteractWith.transform.GetComponent<ICustomDescription>() != null)
        {
            interactionIconText.text = objectToInteractWith.transform.GetComponent<ICustomDescription>().description();
        }
        else
        {
            if (objectToInteractWith.transform.GetComponent<Loot>() != null)
            {
                Loot objectLoot = objectToInteractWith.transform.GetComponent<Loot>();
                interactionIconText.text = objectLoot._name + "\nValue: " + objectLoot._value;

                if (objectToInteractWith.transform.GetComponent<Sword>() != null)
                {
                    interactionIconText.text = interactionIconText.text + " \n Damage: " + objectToInteractWith.transform.GetComponent<Sword>()._damage;
                }

                if (objectToInteractWith.transform.GetComponent<Bow>() != null)
                {
                    interactionIconText.text = interactionIconText.text + " \n Damage: " + objectToInteractWith.transform.GetComponent<Bow>()._damage;
                }

            }
        }
    }

}
