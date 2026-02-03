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
    
    public bool AddFish(FishSO fish)
    {
        Debug.Log("fishadded");
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].fishData == null)
            {
                slots[i].fishData = fish;
                OnInventoryChanged?.Invoke();
                return true;
            }
        }
        return false; 
    }
    
    //precisely remove a fish (which won't have many use case but still useful to have)
    public bool RemoveFish(FishSO fish)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].fishData == fish)
            {
                slots[i].fishData = null;
                OnInventoryChanged?.Invoke();
                return true;
            }
        }
        return false; 
    }

    public FishSO GetFishAt(int index)
    {
        return slots[index].fishData;
    }
    
    //remove fish at index like specific index like which index to remove fish from
    public void RemoveAt(int index)
    {
        if (index >= 0 && index < slots.Count)
        {
            slots[index].fishData = null;
            OnInventoryChanged?.Invoke();
        }
    }
    
    public void SwapSlots(int a, int b)
    {
        var temp = slots[a].fishData;
        slots[a].fishData = slots[b].fishData;
        slots[b].fishData = temp;

        OnInventoryChanged?.Invoke();
    }



}
