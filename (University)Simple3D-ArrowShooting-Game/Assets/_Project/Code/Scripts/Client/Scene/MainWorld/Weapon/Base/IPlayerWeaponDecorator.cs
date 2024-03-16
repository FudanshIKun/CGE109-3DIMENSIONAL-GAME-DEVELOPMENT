using UnityEngine;

namespace Wonderland.Client.MainWorld
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
