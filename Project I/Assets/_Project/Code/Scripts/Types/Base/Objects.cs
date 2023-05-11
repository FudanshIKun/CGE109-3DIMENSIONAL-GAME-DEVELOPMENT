using Sirenix.OdinInspector;
using UnityEngine;

namespace Wonderland.Types
{
    public abstract class Objects : SerializedMonoBehaviour
    {
        public static bool IsBetween(float anyFloat, float min, float max)
        {
            return anyFloat >= min && anyFloat <= max;
        }

        public static bool IsInSpace(Objects anyObject, Vector3 vA, Vector3 vG)
        {
            var position = anyObject.gameObject.transform.position;
            return IsBetween(position.x, vA.x, vG.x)
                   && IsBetween(position.y, vA.y, vG.y)
                   && IsBetween(position.z, vA.z, vG.z);
        }
        
        protected static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    }
}