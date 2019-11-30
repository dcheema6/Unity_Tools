using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text dialogueText;
    public Transform choicesContainer;
    public float choicesContainerHeight;
    public Translation language;
    private Transform choiceTemplate;
    private List<Transform> choiceList;

    void Start()
    {
        choiceTemplate = choicesContainer.Find("ChoiceTemplateButton");
        choiceTemplate.gameObject.SetActive(false);
        choiceList = new List<Transform>();
    }

    public void StartDialogue(DialogueTree dialogue)
    {
        DialogueNode startNode = dialogue.GetStartNode();
        ProcessNode(startNode);
    }

    private void ProcessNode(DialogueNode node)
    {
        ClearChoices();

        string dialogueTextContent = node.dialogueText;
        if (language != null)
        {
            dialogueTextContent = language.Translate(dialogueTextContent);
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogueText, dialogueTextContent));

        foreach (string choiceContent in node.choices)
        {
            Transform choice = Instantiate(choiceTemplate, choicesContainer);
            choice.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, -choicesContainerHeight * choiceList.Count);
            choice.gameObject.SetActive(true);

            StartCoroutine(TypeSentence(choice.Find("Text").GetComponent<Text>(), language.Translate(choiceContent)));

            XNode.NodePort connections = node.GetOutputPort("Choice_" + choiceList.Count);
            Button choiceButton = choice.GetComponent<Button>();
            if (connections.IsConnected)
            {
                DialogueNode choiceOutput = connections.GetConnection(0).node as DialogueNode;
                choiceButton.onClick.AddListener(() => ProcessNode(choiceOutput));
            }
            else
            {
                choiceButton.onClick.AddListener(EndDialogue);
            }
            choiceList.Add(choice);
        }
    }

    IEnumerator TypeSentence(Text field, string sentence)
    {
        field.text = "";
        foreach (char letter in sentence)
        {
            field.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogueText, "End of dialogue"));
        ClearChoices();
    }

    void ClearChoices()
    {
        foreach (Transform choice in choiceList)
        {
            Destroy(choice.gameObject);
        }
        choiceList.Clear();
    }
}
