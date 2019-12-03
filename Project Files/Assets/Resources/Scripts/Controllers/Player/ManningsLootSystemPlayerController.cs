using System.Collections;
using System.Collections.Generic;
using ManningsLootSystem;
using UnityEngine;

public class ManningsLootSystemPlayerController : PlayerController
{


    //create singleton
    public static ManningsLootSystemPlayerController instance;
    private static ManningsLootSystemPlayerController _instance;

    public static ManningsLootSystemPlayerController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ManningsLootSystemPlayerController>();
            }

            return _instance;
        }
    }



    [SerializeField]
    protected Sword equippedSword;
    public Sword _equippedSword { get { return equippedSword; } }

    [SerializeField]
    protected Bow equippedBow;
    public Bow _equippedBow { get { return equippedBow; } }

    [SerializeField]
    protected GameObject bowSlot;
    public GameObject _bowSlot { get { return bowSlot; } }

    [SerializeField]
    protected GameObject swordSlot;
    public GameObject _swordSlot { get { return swordSlot; } }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();   
    }

  

    // Update is called once per frame
    protected override void Update()
    {

        base.Update();
        
        if (Input.GetMouseButtonDown(0))
        {
            if (equippedSword != null)
            {
                //use sword
                equippedSword.Use();
                equippedSword.Show(true);

                //Hide the bow if one is equipped
                if (equippedBow != null)
                {
                    equippedBow.Show(false);

                }

            }
            else
            {
                MessageLog.Instance.UpdateLog("You do not have a sword equipped");
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (equippedBow != null)
            {
                //use bow
                equippedBow.Use();
                equippedBow.Show(true);
                if (equippedSword != null)
                {
                    equippedSword.Show(false);
                }
            }
            else
            {
                MessageLog.Instance.UpdateLog("You do not have a bow equipped");
            }

        }



        if(equippedSword != null)
        {
            equippedSword.SetDirection(moveDirectionNum);
        }
    }





    public void EquipBow(Bow bow)
    {
        if(equippedBow != null)
        {
            equippedBow.Equip(false);
        }

        equippedBow = bow;
        equippedBow.transform.parent = bowSlot.transform;
        equippedBow.transform.position = bowSlot.transform.position;
    }

    public void EquipSword(Sword sword)
    {
        Debug.Log(sword);
        if(equippedSword != null)
        {
            equippedSword.Equip(false);
        }
        equippedSword = sword;
        equippedSword.transform.parent = swordSlot.transform;
        equippedSword.transform.position = swordSlot.transform.position;
    }
}
