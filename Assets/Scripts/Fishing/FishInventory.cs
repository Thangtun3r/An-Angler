using System;
using System.Collections.Generic;
using UnityEngine;

public class FishInventory : MonoBehaviour
{
    public static event Action OnInventoryChanged;

    [SerializeField] private int slotCount = 20;

    private List<InventoryItem> slots;

    void Awake()
    {
        slots = new List<InventoryItem>(slotCount);

        for (int i = 0; i < slotCount; i++)
            slots.Add(new InventoryItem());
    }

    public bool AddItem(ItemSO item)
    {
        if (item == null) return false;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].itemData == null)
            {
                slots[i].itemData = item;
                NotifyChange();
                return true;
            }
        }

        return false; 
    }

    public bool RemoveItem(ItemSO item)
    {
        if (item == null) return false;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].itemData == item)
            {
                slots[i].itemData = null;
                NotifyChange();
                return true;
            }
        }

        return false;
    }

    public ItemSO GetItem(int index)
    {
        if (!IsValidIndex(index)) return null;
        return slots[index].itemData;
    }

    public void RemoveAt(int index)
    {
        if (!IsValidIndex(index)) return;

        if (slots[index].itemData != null)
        {
            slots[index].itemData = null;
            NotifyChange();
        }
    }

    public void SwapSlots(int a, int b)
    {
        if (!IsValidIndex(a) || !IsValidIndex(b) || a == b) return;

        var temp = slots[a].itemData;
        slots[a].itemData = slots[b].itemData;
        slots[b].itemData = temp;

        NotifyChange();
    }

    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < slots.Count;
    }

    private void NotifyChange()
    {
        OnInventoryChanged?.Invoke();
    }
}