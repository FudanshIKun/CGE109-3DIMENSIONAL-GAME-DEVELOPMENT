using System;
using UnityEngine;

namespace Wonderland.Management
{
    [RequireComponent(typeof(InputManager))]
    public class SwipeDetection : Controls
    {
        private const float MinimumDistance = .1f;
        private const float MaximumTime = .5f;
        private const float DirectionThreshold = .9f;

        private Vector2 _startPosition;
        private float _startTime;
        private Vector2 _endPosition;
        private float _endTime;

        public static event Action LeftSwipe;
        public static event Action RightSwipe;
        public static event Action UpSwipe;
        public static event Action DownSwipe;
        
        public override void OnEnable()
        {
            base.OnEnable();
            /*MainManager.Instance.InputManager.OnStartPrimaryTouchEvent += SwipeStart;
            MainManager.Instance.InputManager.OnEndPrimaryTouchEvent += SwipeEnd;*/
        }

        public override void OnDisable()
        {
            base.OnDisable();
            /*MainManager.Instance.InputManager.OnStartPrimaryTouchEvent -= SwipeStart;
            MainManager.Instance.InputManager.OnEndPrimaryTouchEvent -= SwipeEnd;*/
        }
        
        #region Methods
        
        private void SwipeStart(Vector2 screenPosition, GameObject swiped, float time)
        {
            _startPosition = screenPosition;
            _startTime = time;
        }
        
        private void SwipeEnd(Vector2 screenPosition, GameObject swiped, float time)
        {
            _endPosition = screenPosition;
            _endTime = time;
            DetectSwipe();
        }
        
        private void DetectSwipe()
        {
            if (Vector3.Distance(_startPosition, _endPosition) >= MinimumDistance && (_endTime - _startTime) <= MaximumTime)
            {
                Logging.DetectionLogger.Log("Swipe Detected");
                Vector3 direction = _endPosition - _startPosition;
                Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
                SwipeDirection(direction2D);
            }
        }
        
        private void SwipeDirection(Vector2 direction)
        {
            if (Vector2.Dot(Vector2.up, direction) > DirectionThreshold)
            {
                UpSwipe?.Invoke();
                Logging.DetectionLogger.Log("SwipeUp");
            } 
            else if (Vector2.Dot(Vector2.down, direction) > DirectionThreshold)
            {
                DownSwipe?.Invoke();
                Logging.DetectionLogger.Log("SwipeDown");
            } 
            else if (Vector2.Dot(Vector2.left, direction) > DirectionThreshold)
            {
                LeftSwipe?.Invoke();
                Logging.DetectionLogger.Log("SwipeLeft");
            } 
            else if (Vector2.Dot(Vector2.right, direction) > DirectionThreshold)
            {
                RightSwipe?.Invoke();
                Logging.DetectionLogger.Log("SwipeRight");
            } 
        }

        #endregion
    }
}