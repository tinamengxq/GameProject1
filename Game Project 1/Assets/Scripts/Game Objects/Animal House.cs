using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalHouse : MonoBehaviour, IInteractable
{
    public string GetPrompt()
    {
        ItemType held = Inventory.Instance.SelectedItem;
        return held == ItemType.AnimalFood ? "Feed Animals (F)" : "Need Animal Food";
    }

    public void Interact()
    {
        if (Inventory.Instance.SelectedItem != ItemType.AnimalFood) return;
        if (!Inventory.Instance.ConsumeSelected()) return;

        QuestManager.Instance.Complete(QuestType.GrowAnimals);
    }
}
