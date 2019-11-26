using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManningsLootSystem
{

    public class Sword : Loot
    {
        //0 = bottom tier, 3 = top tier
        [SerializeField]
        protected string tier;

        protected int damage;

        protected float attackSpeed;


        [SerializeField]
        private bool isEquipped;

        [SerializeField]
        public RuntimeAnimatorController swordAnimator;


        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        public override void Generate()
        {


            //Find the stat ranges for this swords tier 
            foreach (LootController.SwordStats swordTier in LootController.Instance._swordStats)
            {
                if (swordTier.tier == tier)
                {

                    icon = swordTier.possibleSprites[Random.Range(0, swordTier.possibleSprites.Count)];

                    damage = Random.Range(swordTier.minDamage, swordTier.maxDamage);
                    attackSpeed = Random.Range(swordTier.minAttackSpeed, swordTier.maxAttackSpeed);
                    statsDecided = true;
                    value = swordTier.baseValue + damage + Mathf.RoundToInt(attackSpeed * 5);
                    break;
                }
            }
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();


            if (owner != null)
            {
                //transform.position = owner.transform.position;
            }
        }

        public override void Use()
        {
            anim.SetTrigger("Use");
        }


        //adds item to inventory
        public override void AddToInventory()
        {
            base.AddToInventory();

            anim.runtimeAnimatorController = swordAnimator;
            if (ManningsLootSystemPlayerController.Instance != null)
            {
                if (ManningsLootSystemPlayerController.Instance._equippedSword == null)
                {
                    Equip();
                }
            }
        }


        //call to equip or unequip the sword
        public void Equip(bool set = true)
        {
            if (ManningsLootSystemPlayerController.Instance != null)
            {
                if (set == true)
                {
                    anim.SetBool("isEquipped", true);
                    isEquipped = true;
                    ManningsLootSystemPlayerController.Instance.EquipSword(this);
                }
                else
                {
                    anim.SetBool("isEquipped", false);
                    isEquipped = false;
                }
            }
        }



        public void Show(bool set)
        {
            anim.SetBool("isEquipped", set);

        }
    }
}