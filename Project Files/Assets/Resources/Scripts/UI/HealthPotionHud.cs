using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPotionHud : MonoBehaviour
{
    public int HealthPotionCount;
    public Text HealthPotionCountText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthPotionCountText.text = HealthPotionCount.ToString();
    }
}
