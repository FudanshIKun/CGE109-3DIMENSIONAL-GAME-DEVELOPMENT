using UnityEditor;
using UnityEngine;

namespace Wonderland.Client.MainWorld
{
    [CustomEditor(typeof(DetectionSystem))]
    public class DetectionSystemEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            var detectionSystem = (DetectionSystem)target;
            var player = FindObjectOfType<Player>();
            if (detectionSystem != null && player != null)
            {
                var position = player.transform.position;
                var bodyPosition = player.body.position;
                Handles.color = Color.cyan;
                Handles.DrawWireArc(bodyPosition, Vector3.up, Vector3.forward, 360, detectionSystem.interactionRadius);
                Handles.DrawWireArc(bodyPosition, Vector3.forward, Vector3.up, 360, detectionSystem.interactionRadius);
                Handles.DrawWireArc(bodyPosition, Vector3.right, Vector3.up, 360, detectionSystem.interactionRadius);
                Handles.color = Color.red;
                Handles.DrawWireArc(position, Vector3.up, Vector3.forward, 360, detectionSystem.reachableRadius);
                Handles.DrawWireArc(position, Vector3.forward, Vector3.right, 180, detectionSystem.reachableRadius);
                Handles.DrawWireArc(position, Vector3.right, Vector3.back, 180, detectionSystem.reachableRadius);
                
                foreach (var reachableTarget in DetectionSystem.ReachableTargets)
                {
                    if (reachableTarget.isVisible) Handles.DrawLine(player.transform.position, reachableTarget.transform.position);
                }
            }
        }
    }
}