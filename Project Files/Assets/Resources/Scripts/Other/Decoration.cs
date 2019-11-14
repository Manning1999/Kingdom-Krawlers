using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //if the player is below the decoration, then move the decoration back to appear behind the player
        if(PlayerController.Instance.transform.position.y < transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, PlayerController.Instance.transform.position.z + 0.01f);
        }
        //if the player is above the decoration, then move the decoration back to appear infront the player
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, PlayerController.Instance.transform.position.z - 0.01f);
        }
    }
}
