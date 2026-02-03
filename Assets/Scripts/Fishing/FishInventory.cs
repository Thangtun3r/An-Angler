using System;
using System.Collections.Generic;
using UnityEngine;

public class FishInventory : MonoBehaviour
{

    public static event Action OnInventoryChanged; 
    private List<InventoryItem> slots;
    public int slotCount = 20;
    
    void Awake()
    {
        slots = new List<InventoryItem>(slotCount);
        for (int i = 0; i < slotCount; i++)
        {
            slots.Add(new InventoryItem());
        }
    }
    
    public bool AddItem(ItemSO item)
    {
        Debug.Log("fishadded");
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].itemData == null)
            {
                slots[i].itemData = item;
                OnInventoryChanged?.Invoke();
                return true;
            }
        }
        return false; 
    }
    
    //precisely remove a fish (which won't have many use case but still useful to have)
    public bool RemoveItem(ItemSO fish)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].itemData == fish)
            {
                slots[i].itemData = null;
                OnInventoryChanged?.Invoke();
                return true;
            }
        }
        return false; 
    }

    public ItemSO GetItem(int index)
    {
        return slots[index].itemData;
    }
    
    //remove fish at index like specific index like which index to remove fish from
    public void RemoveAt(int index)
    {
        if (index >= 0 && index < slots.Count)
        {
            slots[index].itemData = null;
            OnInventoryChanged?.Invoke();
        }
    }
    
    public void SwapSlots(int a, int b)
    {
        var temp = slots[a].itemData;
        slots[a].itemData = slots[b].itemData;
        slots[b].itemData = temp;

        OnInventoryChanged?.Invoke();
    }



}
