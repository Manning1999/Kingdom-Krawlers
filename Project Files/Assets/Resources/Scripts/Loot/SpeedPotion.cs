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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Use()
        {
            PlayerController.Instance.ChangeSpeedModifier(speedModifier);
            StartCoroutine(Timer());

        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(duration);
            PlayerController.Instance.ChangeSpeedModifier(1);

        }
    }
}
