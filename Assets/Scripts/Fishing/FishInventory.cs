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

    public FishSO GetFishAt(int slotIndex)
    {
        return slots[slotIndex].fishData;
    }
    
    




}
