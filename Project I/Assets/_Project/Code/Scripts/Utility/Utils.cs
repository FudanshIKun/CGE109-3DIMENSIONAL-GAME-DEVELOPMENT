using UnityEngine;

namespace Wonderland
{
    public class Utils : MonoBehaviour
    {
        public static Vector3 ScreenToCamera(Camera camera, Vector3 position)
        {
            position.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(position);
        }

        public static GameObject ScreenToObject(Camera camera, Vector3 position)
        {
            Ray ray = camera.ScreenPointToRay(position);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
            if (hit.collider != null)
            {
                Debug.DrawLine(position, hit.collider.transform.position);
                return hit.collider.gameObject;
            }
            if (hit2D.collider != null)
            {
                Debug.DrawLine(position, hit2D.collider.transform.position);
                return hit2D.collider.gameObject;
            }
            return null;
        }
    }
}