//This script is used to define the stat ranges for the various types of loot

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{

    //create singleton
    public static LootController instance;
    private static LootController _instance;

    public static LootController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LootController>();
            }

            return _instance;
        }
    }


  
    protected List<Sprite> swordSprites = new List<Sprite>();
    public List<Sprite> _swordSprites { get { return swordSprites; } }


    [System.Serializable]
    public class SwordStats
    {
        public string tier;
        public int minDamage;
        public int maxDamage;

        public float minAttackSpeed;
        public float maxAttackSpeed;

        public int baseValue;


        public List<Sprite> possibleSprites;

    }

    [SerializeField]
    protected List<SwordStats> swordStats = new List<SwordStats>();
    public List<SwordStats> _swordStats { get { return swordStats; } }


    

    [System.Serializable]
    public class BowStats
    {
        public string tier;
        public int minDamage;
        public int maxDamage;

        public float minAttackSpeed;
        public float maxAttackSpeed;

        public int baseValue;
        public List<Sprite> possibleSprites;
    }

    [SerializeField]
    protected List<BowStats> bowStats = new List<BowStats>();
    public List<BowStats> _bowStats { get { return bowStats; } }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
