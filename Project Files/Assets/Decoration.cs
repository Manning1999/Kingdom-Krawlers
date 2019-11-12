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
        if(PlayerController.Instance.transform.position.y < transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, PlayerController.Instance.transform.position.z + 0.01f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, PlayerController.Instance.transform.position.z - 0.01f);
        }
    }
}
