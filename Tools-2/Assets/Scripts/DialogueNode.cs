using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using XNode;
using UnityEngine.UI;

public class DialogueNode : Node {
    [Input] public bool startNode;
    public string dialogueText;
    public List<string> choices;

    public int GetOutputCount()
    {
        return Outputs.Count();
    }
}
