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
    string[] languages;
    int selectedLang;

    public void OnEnable()
    {
        translation = (Translation)target;
        PopulateLanguages();
        selectedLang = 0;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        selectedLang = EditorGUILayout.Popup(selectedLang, languages);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add/Edit Language"))
        {
            if (selectedLang != languages.Length - 1)
                PopupWindow.Show(buttonRect, new AddEditTranslationPopUp(languages[selectedLang]));
            else
                PopupWindow.Show(buttonRect, new AddEditTranslationPopUp());
        }
        if (GUILayout.Button("Refresh"))
        {
            OnEnable();
            OnInspectorGUI();
        }

        GUILayout.EndHorizontal();
        EditorUtility.SetDirty(target);
    }

    void PopulateLanguages()
    {
        List<string> temp = new List<string>();
        using (StreamReader reader = new StreamReader(@"Assets/TranslationSupport/languages.txt"))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line != null && line != "" && line != " ")
                {
                    temp.Add(line);
                }
            }
        }
        temp.Add("Add new language");
        languages = temp.ToArray();
    }
}
