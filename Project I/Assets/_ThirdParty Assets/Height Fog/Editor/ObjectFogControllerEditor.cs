// SKGames vertical fog editor GUI. Copyright (c) 2018 Sergey Klimenko. 18.05.2018

using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectFogController))]
[Obsolete("Obsolete")]
public class ObjectFogControllerEditor : Editor
{
    private GUIStyle _boxStyle;
    private ObjectFogController _targetInstance;

    public override void OnInspectorGUI()
    {
        _boxStyle          = GUI.skin.GetStyle("HelpBox");
        _boxStyle.richText = true;
        _boxStyle.normal.textColor = new Color(0.8f, 0f, 0f, 1f);
        _targetInstance    = (ObjectFogController)target;
        if (_targetInstance.overridedFromGlobalController && GlobalObjectFogController.Exists)
        {
            EditorGUILayout.TextArea("<b>This controller overrided from global controller!</b>", _boxStyle);
            if (GUILayout.Button("Disable override for this item"))
            {
                _targetInstance.overridedFromGlobalController = false;
            }
        }
        else
        {
            DrawDefaultInspector();
            if (!GlobalObjectFogController.Exists)
            {
                EditorGUILayout.TextArea("<b>Global controller not exists or disabled, override can't be applied!</b>", _boxStyle);
            }
            else
            {
                if (GUILayout.Button("Enable override for this item"))
                {
                    _targetInstance.overridedFromGlobalController = true;
                }
            }
            
        }
    }
}
