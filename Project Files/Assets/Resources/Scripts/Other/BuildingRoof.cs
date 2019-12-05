using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingRoof : MonoBehaviour
{
    [SerializeField]
    private TilemapRenderer rend;


    public void Start()
    {
        if (rend == null)
        {
            rend = transform.GetComponent<TilemapRenderer>();
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Player entered");
        if(col.transform.GetComponent<PlayerController>() != null)
        {
            rend.enabled = false;
        }   
    }


    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Player left");
            rend.enabled = true;
        }
    }
}
