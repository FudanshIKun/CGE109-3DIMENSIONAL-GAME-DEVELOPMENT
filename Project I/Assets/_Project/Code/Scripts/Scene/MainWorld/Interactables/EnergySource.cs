using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wonderland.Scene.MainWorld
{
    public class EnergySource : Interactable
    {
        [Header("EnergySource Settings")] 
        [Range(0, 1)] public float energyAmount; 
        
        public override void Interaction()
        {
            CustomLog.GamePlaySystem.Log("Interaction with: " + this);
            isAvailable = false;
            meshRenderer.material.color = Color.black;
            meshRenderer.material.SetColor(EmissionColor, Color.black);
            GameplayHandler.Instance.IncreaseEnergy(energyAmount);
        }
    }
}
