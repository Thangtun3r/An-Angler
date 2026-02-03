using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFishTest : MonoBehaviour
{
    public FishInventory fishInventory;
    public ItemSO testFish;

    public void AddTestFish()
    {
        fishInventory.AddItem(testFish);
    }
}
