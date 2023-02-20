using System.Collections;
using UnityEngine;
using Wonderland.Utility;

namespace Wonderland.GamePlay.InputManagement
{
    [RequireComponent(typeof(InputManager))]
    public class PinchDetection : IControls
    {
        private Vector2 secondTouchPosition;

        private Coroutine ZoomCoroutine;
        private Transform cameraTransform;

        #region Methods

        private void ZoomStart(Vector2 position)
        {
            secondTouchPosition = position;
            ZoomCoroutine = StartCoroutine(Detection());
        }

        private void ZoomEnd(Vector2 position)
        {
            StopCoroutine(ZoomCoroutine);
        }

        public IEnumerator Detection()
        {
            Logging.InputLogger.Log("Pinch Detected");
            float previousDistance = 0f, distance = 0f;
            while (true)
            {
                distance = Vector2.Distance(
                    InputManager.Instance._MobileInput.Touch.PrimaryTouchValue.ReadValue<Vector2>(),
                    InputManager.Instance._MobileInput.Touch.SecondaryTouchValue.ReadValue<Vector2>());
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
            cameraTransform = Camera.main.transform;
            Logging.InputLogger.Log(this + " Has Added In Scene");
        }

        private void OnEnable()
        {
            /*
            InputManager.Instance.OnStartSecondaryTouchEvent += ZoomStart;
            InputManager.Instance.OnEndSecondaryTouchEvent += ZoomEnd;
            */
            Logging.InputLogger.Log(this + "has been enabled");
        }

        private void OnDisable()
        {
            /*
            InputManager.Instance.OnStartSecondaryTouchEvent -= ZoomStart;
            InputManager.Instance.OnEndSecondaryTouchEvent -= ZoomEnd;
            */
            Logging.InputLogger.Log(this + "has been enabled");
        }
    }
}
