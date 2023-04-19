using UnityEngine;

namespace Wonderland.GamePlay.BeatRunner
{
    public class Target : MonoBehaviour
    {
        [Header("Status")] 
        public bool isAvailable = true;
        public bool isReachable = false;
        [Header("Settings")] 
        [SerializeField] private GameObject mesh;
        [Header("Required")] 
        [SerializeField] private Player player;
        [Header("Particle")]
        [SerializeField] private ParticleSystem hitParticle;
        [SerializeField] private Collider detector;

        private void OnEnable()
        {
            player = GameplayHandler.Instance.Player;
        }

        private void Update()
        {
            /*var playerToTarget = transform.position - player.transform.position;
            var isBehindPlayer = (Vector3.Dot(player.transform.forward, playerToTarget) < 0) && player.isRunning;*/
        }

        #region Methods

        private void OnBecomeVisible()
        {
            /*var targetSystem = GameplayHandler.Instance.AimSystem;
            if (targetSystem.targets.Contains(this) || !isAvailable) return;
            targetSystem.targets.Add(this);
            if(isReachable) targetSystem.reachableTargets.Add(this);*/
        }

        private void OnBecomeInvisible()
        {
            /*var targetSystem = GameplayHandler.Instance.AimSystem;
            if (targetSystem.targets.Contains(this))
            {
                targetSystem.targets.Remove(this);
                if (targetSystem.reachableTargets.Contains(this)) targetSystem.reachableTargets.Remove(this);
            }
            
            if (targetSystem.currentTarget == this) targetSystem.StopFocus();*/
        }

        public void DisableTarget(Vector3 dir)
        {
            
        }

        #endregion
    }
}
