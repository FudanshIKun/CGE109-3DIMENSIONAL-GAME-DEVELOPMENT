using System.Collections;
using UnityEngine;

namespace Wonderland
{
    [RequireComponent(typeof(InputManager))]
    public class TouchDetection : IControls
    {
        #region Fields

        [HideInInspector] public bool isTouching;
        [HideInInspector] public float pressingTime;
        [HideInInspector] public GameObject currentTouched;
        private Vector2 startPosition;
        private Vector2 endPosition;

        #endregion

        #region Methods
        
        private void TouchStart(Vector2 screenPosition, GameObject touched, float time)
        {
            isTouching = true;
            startPosition = screenPosition;
            pressingTime = time;
            currentTouched = touched;
            DetectTouch(currentTouched);
            StartCoroutine(TimeCounter());
        }
        
        private IEnumerator TimeCounter()
        {
            while (isTouching)
            {
                yield return null;
            }
        }
        
        private void TouchEnd(Vector2 Position, GameObject touched, float time)
        {
            //Logging.InputControls.Log("Have been Pressed For : " + (int)time);
            isTouching = false;
            UnDetectTouch(currentTouched);
            StopCoroutine(TimeCounter());
        }
        
        private void DetectTouch(GameObject gameObject)
        {
            if ( gameObject != null && gameObject.GetComponent<ITouchable>() != null)
            {
                gameObject.GetComponent<ITouchable>().TouchInteraction();
            }
        }

        private void UnDetectTouch(GameObject gameObject)
        {
            if ( gameObject != null && gameObject.GetComponent<ITouchable>() != null)
            {
                gameObject.GetComponent<ITouchable>().TouchCancleInteraction();
            }

            currentTouched = null;
        }

        #endregion

        public override void OnEnable()
        {
            base.OnEnable();
            MainManager.Instance.inputManager.OnStartPrimaryTouchEvent += TouchStart;
            MainManager.Instance.inputManager.OnEndPrimaryTouchEvent += TouchEnd;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            MainManager.Instance.inputManager.OnStartPrimaryTouchEvent -= TouchStart;
            MainManager.Instance.inputManager.OnEndPrimaryTouchEvent -= TouchEnd;
        }
    }
}
