using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Sprite speaker;
    [TextArea(2, 4)]
    [SerializeField] private string[] lines;

    public string GetPrompt()
    {
        return "Talk (F)";
    }

    public void Interact()
    {
        DialogueManager.Instance.StartDialogue(speaker, lines);
    }
}
