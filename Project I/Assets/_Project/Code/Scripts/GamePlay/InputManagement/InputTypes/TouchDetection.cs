using System.Collections;
using UnityEngine;
using Wonderland.Utility;

namespace Wonderland.GamePlay.InputManagement
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="screenPosition"></param>
        /// <param name="touched"></param>
        /// <param name="time"></param>
        private void TouchStart(Vector2 screenPosition, GameObject touched, float time)
        {
            isTouching = true;
            startPosition = screenPosition;
            pressingTime = time;
            currentTouched = touched;
            DetectTouch(currentTouched);
            StartCoroutine(TimeCounter());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private IEnumerator TimeCounter()
        {
            while (isTouching)
            {
                Logging.InputLogger.Log("IsCounting");
                yield return null;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Position"></param>
        /// <param name="touched"></param>
        /// <param name="time"></param>
        private void TouchEnd(Vector2 Position, GameObject touched, float time)
        {
            Logging.InputLogger.Log("Have been Pressed For : " + (int)time);
            isTouching = false;
            StopCoroutine(TimeCounter());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameObject"></param>
        private void DetectTouch(GameObject gameObject)
        {
            if ( gameObject != null && gameObject.GetComponent<ITouchable>() != null)
            {
                gameObject.GetComponent<ITouchable>().TouchInteraction();
            }
        }

        #endregion

        private void OnEnable()
        {
            InputManager.Instance.OnStartPrimaryTouchEvent += TouchStart;
            InputManager.Instance.OnEndPrimaryTouchEvent += TouchEnd;
            Logging.InputLogger.Log(this + "has been enabled");
        }

        private void OnDisable()
        {
            InputManager.Instance.OnStartPrimaryTouchEvent -= TouchStart;
            InputManager.Instance.OnEndPrimaryTouchEvent -= TouchEnd;
            Logging.InputLogger.Log(this + "has been disabled");
        }
    }
}
