using System.Collections;
using UnityEngine;
using Wonderland.Utility;

namespace Wonderland.GamePlay.Input
{
    [RequireComponent(typeof(InputManager))]
    public class PinchDetection : IControls
    {
        #region Fields
        
        private Vector2 secondTouchPosition;
        private Coroutine ZoomCoroutine;
        private Transform cameraTransform;

        #endregion

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
            Logging.InputControls.Log("Pinch Detected");
            float previousDistance = 0f, distance = 0f;
            while (true)
            {
                distance = Vector2.Distance(
                    InputManager.Instance.HandheldInputAction.Touch.PrimaryTouchValue.ReadValue<Vector2>(),
                    InputManager.Instance.HandheldInputAction.Touch.SecondaryTouchValue.ReadValue<Vector2>());
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
            Logging.InputControls.Log(this + " Has Added In Scene");
        }

        public override void OnEnable()
        {
            base.OnEnable();
            InputManager.Instance.OnStartSecondaryTouchEvent += ZoomStart;
            InputManager.Instance.OnEndSecondaryTouchEvent += ZoomEnd;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            InputManager.Instance.OnStartSecondaryTouchEvent -= ZoomStart;
            InputManager.Instance.OnEndSecondaryTouchEvent -= ZoomEnd;
        }
    }
}
