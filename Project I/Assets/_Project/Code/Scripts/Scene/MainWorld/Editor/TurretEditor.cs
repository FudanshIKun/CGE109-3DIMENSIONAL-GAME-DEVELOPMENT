using System;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Wonderland.Scene.MainWorld.Editor
{
    [CustomEditor(typeof(Turret)), CanEditMultipleObjects]
    public class TurretEditor : UnityEditor.Editor
    {
        private void OnSceneGUI()
        {
            var turret = (Turret)target;
            var turretPos = turret.transform.position;
            Handles.color = Color.red;
            Handles.DrawWireArc(turretPos, Vector3.up, Vector3.forward, 360, turret.shootRange);
            Handles.color = Color.blue;
            if (turret.releasePoint != null && turret.targetPoint != null)
            {
                Handles.DrawLine(turret.releasePoint.position, turret.targetPoint.position);
            }
        }
    }
}