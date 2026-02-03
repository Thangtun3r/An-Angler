using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public FishInventory Inventory { get; private set; }

    private void Awake()
    {
        Inventory = GetComponent<FishInventory>();
    }
}

