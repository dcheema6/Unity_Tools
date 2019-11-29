using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class TranslationEditPopUp : PopupWindowContent
{
    string language = "";
    string[] allLines, translations;

    public override void OnOpen()
    {
        allLines = File.ReadAllLines(@"Assets/TranslationSupport/wordslist.txt");

        translations = new string[allLines.Length];
    }

    public override void OnGUI(Rect rect)
    {
        GUILayout.Label("Add Language", EditorStyles.boldLabel);
        language = EditorGUILayout.TextField("Language", language);
        
        for (int i = 0; i < allLines.Length; i++)
        {
            translations[i] = EditorGUILayout.TextField(allLines[i], translations[i]);
        }

        if (GUILayout.Button("Close"))
        {
            editorWindow.Close();
            OnClose();
        }
    }

    public override void OnClose()
    {
        string langDictPath = @"Assets/TranslationSupport/" + language + ".csv";
        if (File.Exists(langDictPath)) return;
        
        using (StreamWriter dictWriter = new StreamWriter(langDictPath))
        {
            for (int i = 0; i < translations.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(translations[i]))
                    dictWriter.WriteLine(allLines[i] + "," + translations[i]);
            }
        }

        using (StreamWriter langWriter = new StreamWriter(@"Assets/TranslationSupport/langs.csv", true))
        {
            langWriter.WriteLine(language);
        }

        Debug.Log("Language " + language + " successfully added.");
    }
}
