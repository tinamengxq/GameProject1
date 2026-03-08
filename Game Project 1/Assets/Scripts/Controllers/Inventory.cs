using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    VegetableSeeds,
    Water,
    Vegetable,
    AnimalFood,
    Honey
}

public class Inventory: MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] private int slots = 5;

    private ItemType[] items;
    private int selectedIndex = 0;

    public event Action<ItemType[]> OnInventoryChanged;
    public event Action<int> OnSelectionChanged;

    public ItemType SelectedItem => items[selectedIndex];

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        items = new ItemType[slots];
        for (int i = 0; i < slots; i++) items[i] = ItemType.None;
    }

    private void Start()
    {       
        AddItem(ItemType.VegetableSeeds);
        AddItem(ItemType.Water);
        AddItem(ItemType.AnimalFood);

        NotifyInventory();
        OnSelectionChanged?.Invoke(selectedIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Select(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) Select(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Select(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) Select(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) Select(4);
    }

    public bool AddItem(ItemType item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == ItemType.None)
            {
                items[i] = item;
                NotifyInventory();
                return true;
            }
        }
        return false;
    }

    public bool ConsumeSelected()
    {
        if (items[selectedIndex] == ItemType.None) return false;
        items[selectedIndex] = ItemType.None;
        NotifyInventory();
        return true;
    }
    
    public void Select(int index)
    {
        if (index < 0 || index >= items.Length) return;
        selectedIndex = index;
        OnSelectionChanged?.Invoke(selectedIndex);
    }

    private void NotifyInventory()
    {
        OnInventoryChanged?.Invoke((ItemType[])items.Clone());
    }

    public ItemType GetItem(int index)
    {
        if (index < 0 || index >= items.Length) return ItemType.None;
        return items[index];
    }

    public bool HasItem(ItemType item)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i] == item)
            {
                return true;
            }
        }
        return false;
    }


}
