using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFish", menuName = "Fishing/Fish", order = 1)]
public class FishSO : ScriptableObject
{
    public string fishname;
    public GameObject fishprefab;
    
}

