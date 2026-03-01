using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pond : MonoBehaviour, IInteractable
{
    public string GetPrompt()
    {
        return "Get Water (F)";
    }

    public void Interact()
    {
        // Add one Water item to the first empty slot.
        bool added = Inventory.Instance.AddItem(ItemType.Water);

        // Optional: if bag full, show a quick dialogue instead of silently failing
        if (!added)
        {
            DialogueManager.Instance.StartDialogue(
                null,
                new string[] { "My bag is full. I can't carry more water." }
            );
        }
    }
}
