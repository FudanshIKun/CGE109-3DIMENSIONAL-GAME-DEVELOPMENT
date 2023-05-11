using UnityEngine;

namespace Wonderland.Scene.MainWorld
{
    public interface IWeaponDecorator
    {
        public void Releasing();

        public void TargetHit(Vector3 dir);
        public void StartCharge();
        public void Charging();
        public void ReleaseCharge();
    }
}
