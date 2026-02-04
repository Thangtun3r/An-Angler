using System;
using DG.Tweening;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public FishInventory Inventory { get; private set; }

    public RectTransform uiInventoryParent;
    public Vector2 hiddenPosition;

    private Vector2 defaultPosition;
    private bool isOpen;
    private Tween inventoryTween;
    
    public static event Action<bool> OnInventoryToggled; 

    
    private void Awake()
    {
        
        Inventory = GetComponent<FishInventory>();

        if (uiInventoryParent == null) return;

        defaultPosition = uiInventoryParent.anchoredPosition;
        uiInventoryParent.anchoredPosition = hiddenPosition;
        isOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        if (uiInventoryParent == null) return;

        inventoryTween?.Kill();

        Vector2 targetPos = isOpen ? hiddenPosition : defaultPosition;
        Ease ease = isOpen ? Ease.InCubic : Ease.OutCubic;

        inventoryTween = uiInventoryParent
            .DOAnchorPos(targetPos, 0.2f)
            .SetEase(ease);

        isOpen = !isOpen;

        OnInventoryToggled?.Invoke(isOpen);
    }

}