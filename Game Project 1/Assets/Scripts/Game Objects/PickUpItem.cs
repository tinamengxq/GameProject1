using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour, IInteractable
{
    public ItemType itemType;
    public int amount = 1;

    public void Interact(Player player)
    {
        if(itemType == ItemType.Seed)
        {
            player.inventory.seeds += amount;
            Destroy(gameObject);
        }

        if(itemType == ItemType.Watercan)
        {
            player.inventory.hasWaterCan = true;
            Destroy(gameObject);
        }
    }

    public string GetPrompt(Player player)
    {
        return itemType == ItemType.Seed ? "Press F to pick up Seeds" : "Press F to pick up Watering Can";
    }
}
