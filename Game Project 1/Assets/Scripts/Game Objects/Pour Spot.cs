using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourSpot : MonoBehaviour, IInteractable
{
    public string GetPrompt()
    {
        return Inventory.Instance.SelectedItem == ItemType.Water
            ? "Pour Water (F)"
            : "Need Water";
    }

    public void Interact()
    {
        if (Inventory.Instance.SelectedItem != ItemType.Water) return;
        Inventory.Instance.ConsumeSelected();

        // Optional feedback
        DialogueManager.Instance.StartDialogue(
            null,
            new string[] { "You poured some water." }
        );
    }
}
