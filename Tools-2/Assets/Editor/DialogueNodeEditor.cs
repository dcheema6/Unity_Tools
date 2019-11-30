using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

[CustomEditor(typeof(DialogueNode))]
public class DialogueNodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DialogueNode node = (DialogueNode)target;
        int outputCount = node.GetOutputCount();
        int missingOutputCount = node.choices.Count - outputCount;

        if (missingOutputCount > 0)
        {
            for (int i = 0; i < missingOutputCount; i++)
            {
                node.AddDynamicOutput(typeof(DialogueNode), fieldName: "Choice_" + (outputCount + i));
            }
        }
        else if (missingOutputCount < 0)
        {
            for (int i = 1; i < -missingOutputCount + 1; i++)
            {
                node.RemoveDynamicPort("Choice_" + (outputCount - i));
            }
        }
    }
}
