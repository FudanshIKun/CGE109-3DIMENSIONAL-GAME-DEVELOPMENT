using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Wonderland.Scene.MainWorld
{
    internal class EnergySystem : System
    {
        [FormerlySerializedAs("EnergyLevel")]
        [Header("Status")] 
        [ReadOnly] public float energyLevel;
        [Header("Settings")] 
        [SerializeField] private float energyChargeDuration;
        [SerializeField] private Ease chargeEase;
        [Header("Required Components")]
        [Header("UI")] 
        [SerializeField] private Slider energyChargeBar;

        public void IncreaseEnergyLevel(float amount)
        {
            var currentEnergyLevel = energyLevel;
            amount = Mathf.Clamp(amount, 0f, 1f);
            DOVirtual.Float(currentEnergyLevel, currentEnergyLevel + amount, energyChargeDuration, SetEnergyLevel).SetId(0).SetEase(chargeEase);
        }

        private void SetEnergyLevel(float charge)
        {
            energyLevel = charge;
            energyChargeBar.value = energyLevel;
        }

        public void DecreaseEnergyLevel(float amount)
        {
            var currentEnergyLevel = energyLevel;
            amount = Mathf.Clamp(amount, 0, 1);
            DOVirtual.Float(currentEnergyLevel, currentEnergyLevel - amount, energyChargeDuration, SetEnergyLevel).SetId(0).SetEase(chargeEase);
        }
    }
}
