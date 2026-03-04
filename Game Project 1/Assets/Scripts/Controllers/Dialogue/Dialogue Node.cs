using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNode : ScriptableObject
{
    [Header("Speaker")]
    public Sprite speakerSprite;
    public string speakerName;

    [Header("Lines")]
    [TextArea(2, 5)]
    public string[] lines;

    [Header("Choices (optional)")]
    public DialogueChoice[] choices;
}
