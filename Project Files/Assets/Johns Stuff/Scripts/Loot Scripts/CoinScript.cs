using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int amountOfCoin;
    public int coinsMin = 5;
    public int coinsMax = 15;


    // Start is called before the first frame update
    void Start()
    {
        amountOfCoin = Random.Range(coinsMin, coinsMax);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
