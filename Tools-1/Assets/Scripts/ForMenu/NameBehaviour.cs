using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameBehaviour : MonoBehaviour
{
    public string name;

    [ContextMenuItem("Randomize Name", "Randomize")]
    public string otherName;

    [ContextMenu("Reset Name")]
    private void ResetName()
    {
        name = string.Empty;
    }

    private void Randomize()
    {
        otherName = "Some Random Name";
    }
}
