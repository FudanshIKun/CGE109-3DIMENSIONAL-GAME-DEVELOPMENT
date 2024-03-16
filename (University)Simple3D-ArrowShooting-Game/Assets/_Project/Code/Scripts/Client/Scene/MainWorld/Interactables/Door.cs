using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Wonderland.Client.MainWorld
{
    public class Door : Interactable
    {
        [ReadOnly] [SerializeField] private bool endgame;
        [ReadOnly] public static int turretAmount;

        private void Start()
        {
            turretAmount = FindObjectsOfType<Turret>().Length;
        }

        private void Update()
        {
            if (turretAmount < 1) endgame = true;
        }

        public override void Interaction()
        {
            if (endgame) TimelineHandler.Instance.PlayMain();
        }
    }
}
