using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManningsLootSystem
{
    public class SpeedPotion : Potion
    {

        [SerializeField]
        private int duration;

        [SerializeField]
        private float speedModifier;


        public override void Use()
        {
            base.Use();


            PlayerController.Instance.ChangeSpeedModifier(speedModifier);
            StartCoroutine(Timer());

        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(duration);
            Debug.Log("Time is up");
            PlayerController.Instance.ChangeSpeedModifier(1);

        }
    }
}
