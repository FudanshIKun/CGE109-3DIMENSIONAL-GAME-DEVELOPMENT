using System.Collections;
using UnityEngine;
using Wonderland.InputActions;
using Wonderland.Manager;

namespace Wonderland.GamePlay.InputManagement
{
    public class PinchDetection : MonoBehaviour, IControls
    {
        private MobileInputActions _mainInputActions;
        private Coroutine ZoomCoroutine;
        private Transform cameraTransform;

        #region Methods

        private void ZoomStart()
        {
            ZoomCoroutine = StartCoroutine(Detection());
        }

        private void ZoomEnd()
        {
            StopCoroutine(ZoomCoroutine);
        }

        public IEnumerator Detection()
        {
            float previousDistance = 0f, distance = 0f;
            while (true)
            {
                distance = Vector2.Distance(
                    _mainInputActions.Touch.PrimaryFingerValue.ReadValue<Vector2>(),
                    _mainInputActions.Touch.SecondaryFingerValue.ReadValue<Vector2>());
                if (distance > previousDistance)
                {
                    Vector3 targetPosition = cameraTransform.position;
                    targetPosition.z -= 1;
                    Camera.main.orthographicSize++;
                }else if (distance < previousDistance)
                {
                    Vector3 targetPosition = cameraTransform.position;
                    targetPosition.z += 1;
                    Camera.main.orthographicSize--;
                }

                previousDistance = distance;
                yield return null;
            }
        }
        
        #endregion

        private void Awake()
        {
            _mainInputActions = InputManager.Instance._MobileInput;
            cameraTransform = Camera.main.transform;
            Logging.InputLogger.Log(this + " Has Added In Scene");
        }

        private void OnEnable()
        {
            _mainInputActions.Touch.SecondaryTouchButton.started += _ => ZoomStart();
            _mainInputActions.Touch.SecondaryTouchButton.canceled += _ => ZoomEnd();
        }

        private void OnDestroy()
        {
            Logging.InputLogger.Log(this + " Has Destroyed From Scene");
        }
    }
}
