using UnityEngine;
using UnityEngine.Playables;

namespace Wonderland.Client.MainWorld
{
    public class TimelineHandler : Handler
    {
        public static TimelineHandler Instance { get; private set; }
        [SerializeField] private PlayableDirector timeline;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public void PlayMain()
        {
            timeline.Play();
        }
    }
}