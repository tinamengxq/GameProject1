using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Seed,
    Watercan
}

[System.Serializable]
public class Inventory
{
    public int seeds = 0;

    public bool hasWaterCan = false;
    public int water = 0;
    public int waterCapacity = 3;

    public bool CanWater(int amount) => hasWaterCan && water >= amount;
    public void AddWater(int amount) => water = Mathf.Clamp(water + amount, 0, waterCapacity);
    public void UseWater(int amount) => water = Mathf.Max(0, water - amount);

}
