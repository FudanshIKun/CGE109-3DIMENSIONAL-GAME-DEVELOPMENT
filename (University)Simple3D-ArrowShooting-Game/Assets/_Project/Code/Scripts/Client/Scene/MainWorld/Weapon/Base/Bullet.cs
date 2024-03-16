using System;
using UnityEngine;

namespace Wonderland.Client.MainWorld
{
    [Serializable]
    public class Bullet
    {
        [Header("Settings")] 
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
        public float lifeTime;
        public TrailRenderer trailEffect;
    }
}
