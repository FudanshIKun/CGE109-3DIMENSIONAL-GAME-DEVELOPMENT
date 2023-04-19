using UnityEngine;

namespace Wonderland
{
    public class Utils : MonoBehaviour
    {
        public static GameObject ScreenToObject(Camera camera, Vector3 position)
        {
            var ray = camera.ScreenPointToRay(position);
            Physics.Raycast(ray, out var hit);

            if (hit.collider != null)
            {
                Debug.DrawLine(position, hit.collider.transform.position);
                return hit.collider.gameObject;
            }
            
            var hit2D = Physics2D.GetRayIntersection(ray);
            
            if (hit2D.collider != null)
            {
                Debug.DrawLine(position, hit2D.collider.transform.position);
                return hit2D.collider.gameObject;
            }
            return null;
        }
    }
}