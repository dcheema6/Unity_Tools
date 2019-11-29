using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class AddEditTranslationPopUp : PopupWindowContent
{
    string language = "";
    string[] words, translations;
    bool isNew;

    public AddEditTranslationPopUp() : base()
    {
        isNew = true;
    }

    public AddEditTranslationPopUp(string language) : base()
    {
        this.language = language;
        isNew = false;
    }

    public override void OnOpen()
    {
        words = File.ReadAllLines("Assets/TranslationSupport/words.txt");
        translations = new string[words.Length];
        if (language != "")
        {
            Dictionary<string, string> dict = Translation.BuildDictionary(language);
            for (int i = 0; i < words.Length; i++)
            {
                if (dict.TryGetValue(words[i], out string temp))
                    translations[i] = temp;
                else
                    translations[i] = words[i];
            }
        }
    }

    public override void OnGUI(Rect rect)
    {
        GUILayout.Label("Add/Edit Language", EditorStyles.boldLabel);
        language = EditorGUILayout.TextField("Language", language);
        
        for (int i = 0; i < words.Length; i++)
        {
            translations[i] = EditorGUILayout.TextField(words[i], translations[i]);
        }

        if (GUILayout.Button("Save"))
        {
            editorWindow.Close();
            OnSave();
        }

        if (GUILayout.Button("Close"))
        {
            editorWindow.Close();
        }
    }

    public void OnSave()
    {
        using (StreamWriter dictWriter = new StreamWriter("Assets/TranslationSupport/" + language + ".csv", false))
        {
            for (int i = 0; i < translations.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(translations[i]))
                    dictWriter.WriteLine(words[i] + "," + translations[i]);
            }
        }

        if (isNew)
        {
            using (StreamWriter langWriter = new StreamWriter("Assets/TranslationSupport/languages.txt", true))
            {
                langWriter.WriteLine(language);
            }
            Debug.Log("Language " + language + " successfully added.");
        }
        else
            Debug.Log("Language " + language + " successfully modified.");
    }
}
