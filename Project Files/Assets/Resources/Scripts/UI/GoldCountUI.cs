using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCountUI : MonoBehaviour
{


    //create singleton
    public static GoldCountUI instance;
    private static GoldCountUI _instance;

    public static GoldCountUI Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GoldCountUI>();
            }

            return _instance;
        }
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //gold = PlayerController.Instance._gold;
        
    }


    public void UpdateGoldCount(int goldCount)
    {

        GetComponent<Text>().text = goldCount.ToString();
    }
}
