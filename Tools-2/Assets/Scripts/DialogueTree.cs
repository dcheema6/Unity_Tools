using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;

[CreateAssetMenu]
public class DialogueTree : NodeGraph
{
    public DialogueNode GetStartNode()
    {
        foreach (DialogueNode node in nodes)
        {
            if (node != null && node.startNode) return node;
        }
        return null;
    }
}
