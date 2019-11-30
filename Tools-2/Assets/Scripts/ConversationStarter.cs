using UnityEngine;

public class ConversationStarter : MonoBehaviour
{
    public DialogueTree dialogue;

    public void StartConversation()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
