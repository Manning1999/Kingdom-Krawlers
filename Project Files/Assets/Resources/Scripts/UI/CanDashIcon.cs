using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanDashIcon : MonoBehaviour
{
    //public GameObject Player;
    public Material statusMaterial;
    
    private bool canDash;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        // Player.GetComponent<PlayerController>();
        if (Input.GetButtonDown("Fire2"))
        {
            canDash = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            canDash = false;
        }


        if (canDash == true)
        {
            
            GetComponent<Image>().color = Color.green;
        }

        if (canDash == false)
        {
            
            GetComponent<Image>().color = Color.red;
        }

        
        

    }
}
