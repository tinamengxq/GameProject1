using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    public string choiceText;

    [Tooltip("Next node when selected. If null, dialogue ends.")]
    public DialogueNode nextNode;
}
