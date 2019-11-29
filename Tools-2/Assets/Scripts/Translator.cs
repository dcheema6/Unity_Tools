using UnityEngine;
using UnityEngine.UI;

public class Translator : MonoBehaviour
{
    [SerializeField] Translation translator;

    void Start()
    {
        Translate();
    }

    void Translate()
    {
        if (translator != null)
        {
            Text uiText = GetComponent<Text>();
            uiText.text = translator.Translate(uiText.text);
        }
    }
}
