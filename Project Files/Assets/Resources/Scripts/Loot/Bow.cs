using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManningsLootSystem
{
    public class Bow : Loot, IInteractable
    {

        [SerializeField]
        protected string tier;

        [SerializeField]
        protected int damage;

        public int _damage { get { return damage; } }

        [SerializeField]
        protected float attackSpeed;


        [SerializeField]
        protected GameObject arrow = null;


        [SerializeField]
        private bool isEquipped;



        [SerializeField]
        protected Vector3 aimingDirection;

        private bool canAttack = true;



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

            if (ManningsLootSystemPlayerController.Instance._equippedBow == null)
            {
                Equip();
            }
        }

        // Update is called once per frame
        protected override void Update()
        {

            base.Update();


            if (owner != null)
            {
                transform.position = owner.transform.position - transform.forward * 0.05f; ;
            }
        }




        public void Use()
        {
            if (canAttack == true && InventoryController.Instance._arrowCount > 0)
            {
                //shoot an arrow
                Debug.Log("Shot an arrow" + transform.parent.transform.eulerAngles.z);
                GameObject newArrow = Instantiate(arrow, transform.position + transform.right * 0.15f, transform.rotation) as GameObject;
                newArrow.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.eulerAngles.z - 90));
                StartCoroutine(AttackSpeedTimer());
                InventoryController.Instance.UpdateArrows(-1);
            }
            else
            {
                if(InventoryController.Instance._arrowCount <= 0)
                {
                    MessageLog.Instance.UpdateLog("You do not have any arrows left");
                }
            }
        }


        public void Equip(bool set = true)
        {
            if (set == true)
            {

                isEquipped = true;
                ManningsLootSystemPlayerController.Instance.EquipBow(this);

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


        protected IEnumerator AttackSpeedTimer()
        {
            canAttack = false;
            yield return new WaitForSeconds(attackSpeed);
            canAttack = true;
        }
    }
}