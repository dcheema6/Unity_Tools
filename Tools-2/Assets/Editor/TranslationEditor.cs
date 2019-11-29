using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Translation))]
public class TranslationEditor : Editor
{
    Translation translation;
    Rect buttonRect;

    public void OnEnable()
    {
        translation = (Translation)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Language"))
        {
            PopupWindow.Show(buttonRect, new TranslationEditPopUp());
        }
        if (GUILayout.Button("Refresh"))
        {
            OnEnable();
            OnInspectorGUI();
        }
        GUILayout.EndHorizontal();
        EditorUtility.SetDirty(target);
    }
}
