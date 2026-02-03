using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFishInventory : MonoBehaviour
{
    public FishInventory fishInventory;
    private UIFishSlot[] fishSlots;


    private void OnEnable()
    {
        FishInventory.OnInventoryChanged += RefreshUI;
    }
    private void OnDisable()
    {
        FishInventory.OnInventoryChanged -= RefreshUI;
    }
    

    void Start()
    {
        fishSlots = GetComponentsInChildren<UIFishSlot>();

        for (int i = 0; i < fishSlots.Length; i++)
        {
            fishSlots[i].Initialize(i, fishInventory);
        }
    }
    
    void RefreshUI()
    {
        for (int i = 0; i < fishSlots.Length; i++)
        {
            fishSlots[i].RefreshSlot();
        }
    }
    
    
    
    
    
    
    
    
    
}
