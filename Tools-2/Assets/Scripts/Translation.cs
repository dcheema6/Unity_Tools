using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Translation", menuName = "Language/Translation", order = 1)]
public class Translation : ScriptableObject
{
    [SerializeField] string selectedLanguage;
    public string Language
    {
        get { return selectedLanguage; }
    }

    Dictionary<string, string> dictionary;

    public string Translate(string line)
    {
        if (line == null || line == "" || line == " ")
        {
            return "";
        }

        if (dictionary == null)
        {
            dictionary = Translation.BuildDictionary(selectedLanguage);
        }

        string[] words = line.Split(' ');
        string[] translations = new string[words.Length];

        for (int i = 0; i < words.Length; i++)
        {
            if (dictionary.TryGetValue(words[i], out string temp))
                translations[i] = temp;
            else
                translations[i] = words[i];
        }

        return string.Join(" ", translations);
    }

    public static Dictionary<string, string> BuildDictionary(string language)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        using (StreamReader reader = new StreamReader(@"Assets/TranslationSupport/" + language + ".csv"))
        {
            while (!reader.EndOfStream)
            {
                string[] temp = reader.ReadLine().Split(',');
                if (temp.Length == 2)
                {
                    string word = temp[0];
                    string translation = temp[1];
                    dictionary.Add(word, translation);
                }
            }
        }
        return dictionary;
    }
}
