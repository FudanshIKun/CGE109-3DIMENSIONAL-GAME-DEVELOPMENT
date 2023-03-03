using UnityEngine;

namespace Wonderland.GamePlay.NetRunning
{
    public class CameraController : MonoBehaviour
    {
        private Runner _target;
        private Transform _cameraTransform;

        private void Awake()
        {
            _target = GameObject.FindWithTag("Player").GetComponent<Runner>();
            _cameraTransform = _target.gameObject.transform.GetChild(0).GetComponent<Transform>();
        }

        void Update()
        {
            
        }
    }
}
