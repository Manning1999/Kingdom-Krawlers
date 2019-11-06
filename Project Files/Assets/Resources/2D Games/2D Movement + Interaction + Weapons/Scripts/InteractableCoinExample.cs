using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCoinExample : StandardInteractable
{


    bool interactedWith = false;




    public override void InteractWith()
    {

        interactedWith = !interactedWith;

        if (interactedWith == true) {
            transform.position = new Vector3(transform.position.x - 13, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + 13, transform.position.y, transform.position.z);
        }
    }
}
