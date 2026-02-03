using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFishSlot : MonoBehaviour
{
    private string fishName;
    private Image fishImage;
    private FishInventory fishInventory;
    private int slotIndex;
    private FishSO currentFish;

    private void Start()
    {
        fishImage = GetComponent<Image>();
    }

    public void Initialize(int index, FishInventory fishInven)
    {
        slotIndex = index;
        fishInventory = fishInven;
        
    }

    public void RefreshSlot()
    {
        Debug.Log("RefreshSlot");
        
        currentFish = fishInventory.GetFishAt(slotIndex);
        if (currentFish == null)
        {
            fishName = "Empty";
            fishImage.sprite = null;
            return;
        }
        fishName = currentFish.fishname;
        fishImage.sprite = currentFish.fishimage;
    }
}
