using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    VegetableSeed,
    Vegetable,
    Water,
    AnimalFood,
    Honey
}

public class ItemsController : MonoBehaviour
{
    public static ItemsController Instance {get; private set;}

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private Dictionary<Item, int> items = new Dictionary<Item, int>();

    public void AddItem(Item item, int amount)
    {
        items[item] += amount;
        UIController.Instance.UpdateBagUI();
    }

    public bool UseItem(Item item, int amount)
    {
        if(items[item] >= amount)
        {
            items[item] -= amount;
            UIController.Instance.UpdateBagUI();
            return true;
        }
        else
        {
            return false;
        }
    }
}
