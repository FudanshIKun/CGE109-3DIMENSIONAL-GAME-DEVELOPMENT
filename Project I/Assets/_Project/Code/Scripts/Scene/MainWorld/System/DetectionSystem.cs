using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Wonderland.Scene.MainWorld
{
    public class DetectionSystem : System
    {
        [Header("Status")]
        [ReadOnly] public static readonly List<Target> ReachableTargets = new();
        public static readonly Dictionary<int, Interactable> InteractableObjects = new ();
        
        [Header("Settings")]
        [Header("Field Of View")]
        public float reachableRadius;
        [Header("Interaction")]
        public float interactionRadius;

        #region Methods

        public void Detection()
        {
            FindTargets();
            FindInteractables();
        }

        private void FindTargets()
        {
            ReachableTargets.Clear();
            var targetsInViewRadius = new Collider[50];
            var numTargets = Physics.OverlapSphereNonAlloc(GameplayHandler.Instance.player.transform.position, reachableRadius, targetsInViewRadius, GameplayHandler.Instance.setting.aiming.targetLayerMask);
            for (var i = 0; i < numTargets; i++)
            {
                if (!targetsInViewRadius[i].TryGetComponent(out Target target)) continue;
                if (!target.isAvailable) continue;

                if (target.isVisible) ReachableTargets.Add(target);
            }
        }

        private void FindInteractables()
        {
            InteractableObjects.Clear();
    
            var interactableInRadius = new Collider[50];
            var numInteractables = Physics.OverlapSphereNonAlloc(GameplayHandler.Instance.player.body.position, interactionRadius, interactableInRadius, GameplayHandler.Instance.setting.interaction.interactableLayerMask);
    
            for (var i = 0; i < numInteractables; i++)
            {
                if (!interactableInRadius[i].TryGetComponent(out Interactable interactable)) continue;
                if (!interactable.isAvailable) continue;

                if (!InteractableObjects.ContainsKey(interactable.id))
                {
                    InteractableObjects.Add(interactable.id, interactable);
                }
            }
        }

        #endregion
    }
}
