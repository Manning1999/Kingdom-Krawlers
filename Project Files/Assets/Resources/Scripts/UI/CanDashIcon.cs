using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanDashIcon : MonoBehaviour
{
    //public GameObject Player;
    public Material statusMaterial;
    public Sprite canDashImage;
    public Sprite canNotDashImage;

    protected bool canDash;
    
   

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        canDash = PlayerController.Instance._canDash;
        if (canDash == true)
        {
            
            GetComponent<Image>().sprite = canDashImage;
        }

        if (canDash == false)
        {
            
            GetComponent<Image>().sprite = canNotDashImage;
        }

        
        

    }
}
