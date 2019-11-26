using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManningsLootSystem
{

    public class Potion : Loot
    {

        [SerializeField]
        protected AudioClip useSound;

        protected AudioSource audio;

        protected void Start()
        {
            base.Start();
        }

        public void Use()
        {

        }

    }

}