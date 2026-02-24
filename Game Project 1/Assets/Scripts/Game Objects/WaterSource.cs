using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSource : MonoBehaviour, IInteractable
{
    public int fillAmount = 5;
    public void Interact(Player player)
    {
        if (!player.inventory.hasWaterCan)
        {
            return;
        }
        player.inventory.water = player.inventory.waterCapacity;
    }

    public string GetPrompt(Player player)
    {
        if (!player.inventory.hasWaterCan)
        {
            return "Need a Watering Can";
        } 
        return $"Press F to fill water ({player.inventory.water}/{player.inventory.waterCapacity})";
    }
}
