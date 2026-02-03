using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewFish", menuName = "Fishing/Fish", order = 1)]
public class FishSO : ScriptableObject
{
    public string fishname;
    public Sprite fishimage;
    public GameObject fishprefab;
    
}

