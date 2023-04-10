using UnityEngine;

namespace Wonderland.Types
{
    public class Objects : MonoBehaviour
    {
        public bool IsBetween(float anyFloat, float min, float max)
        {
            if (anyFloat >= min && anyFloat <= max) return true;
            return false;
        }

        public bool IsInSpace(Objects anyObject, Vector3 vA, Vector3 vG)
        {
            if (anyObject.IsBetween(anyObject.gameObject.transform.position.x, vA.x, vG.x)
                && anyObject.IsBetween(anyObject.gameObject.transform.position.y, vA.y, vG.y)
                && anyObject.IsBetween(anyObject.gameObject.transform.position.z, vA.z, vG.z))
            {
                return true;
            }
            
            return false;
        }
    }
}