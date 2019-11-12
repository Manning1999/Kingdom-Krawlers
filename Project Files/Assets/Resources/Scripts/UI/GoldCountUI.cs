using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCountUI : MonoBehaviour
{
    protected int gold;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //gold = PlayerController.Instance._gold;
        GetComponent<Text>().text = gold.ToString();
    }
}
