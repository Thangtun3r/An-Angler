using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFishTest : MonoBehaviour
{
    public FishInventory fishInventory;
    public FishSO testFish;

    public void AddTestFish()
    {
        fishInventory.AddFish(testFish);
    }
}
