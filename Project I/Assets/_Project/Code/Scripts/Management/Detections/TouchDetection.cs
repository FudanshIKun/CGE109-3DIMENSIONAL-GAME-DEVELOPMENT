using System.Collections;
using UnityEngine;
using Wonderland.Objects;

namespace Wonderland.Management
{
    [RequireComponent(typeof(InputManager))]
    public class TouchDetection : Controls
    {
        #region Fields

        [HideInInspector] public bool isTouching;
        [HideInInspector] public GameObject currentTouched;

        #endregion

        #region Methods
        
        private void TouchStart(Vector2 screenPosition, GameObject touched, float time)
        {
            isTouching = true;
            currentTouched = touched;
            DetectTouch(currentTouched);
        }

        private void TouchEnd(Vector2 position, GameObject touched, float time)
        {
            isTouching = false;
            UnDetectTouch(currentTouched);
        }
        
        private void DetectTouch(GameObject touchedObject)
        {
            if ( touchedObject != null && touchedObject.GetComponent<ITouchable>() != null)
            {
                touchedObject.GetComponent<ITouchable>().TouchInteraction();
            }
        }

        private void UnDetectTouch(GameObject touchedObject)
        {
            if ( touchedObject != null && touchedObject.GetComponent<ITouchable>() != null)
            {
                touchedObject.GetComponent<ITouchable>().TouchCancleInteraction();
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
