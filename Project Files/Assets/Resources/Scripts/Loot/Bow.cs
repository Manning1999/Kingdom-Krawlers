using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Loot
{

    [SerializeField]
    protected string tier;

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected float attackSpeed;


    [SerializeField]
    protected GameObject arrow = null;


    [SerializeField]
    private bool isEquipped;



    [SerializeField]
    protected Vector3 aimingDirection;



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();    
    }


    public override void Generate()
    {



        foreach (LootController.BowStats bowTier in LootController.Instance._bowStats)
        {
            if (bowTier.tier == tier)
            {
                Debug.Log("generating stats now");
                icon = bowTier.possibleSprites[Random.Range(0, bowTier.possibleSprites.Count)];

                damage = Random.Range(bowTier.minDamage, bowTier.maxDamage);
                attackSpeed = Random.Range(bowTier.minAttackSpeed, bowTier.maxAttackSpeed);
                statsDecided = true;

                value = bowTier.baseValue + damage + Mathf.RoundToInt(attackSpeed * 5);

                break;
            }
        }
    }


    public override void AddToInventory()
    {
        base.AddToInventory();

        if(PlayerController.Instance._equippedBow == null)
        {
            Equip();
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(owner != null)
        {
            transform.position = owner.transform.position;
        }        
    }




    public override void Use()
    {
        //shoot an arrow
        Debug.Log("Shot an arrow");
    }


    public void Equip(bool set = true)
    {
        if(set == true)
        {
            
            isEquipped = true;
            PlayerController.Instance.EquipBow(this);
            
        }
        else
        {
            
            isEquipped = false;
        }
    }



    public void Show(bool set)
    {
        anim.SetBool("isEquipped", set);

    }
}
