using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIFishSlot : MonoBehaviour, IPointerClickHandler
{
    private string fishName;
    public Image fishImage;
    public FishInventory fishInventory;
    private int slotIndex;
    private ItemSO currentFish;

    public bool isDiscardSlot;
    public static int selectedIndex = -1;

    public void Initialize(int index, FishInventory fishInven)
    {
        slotIndex = index;
        fishInventory = fishInven;
    }

    public void OnPointerClick(PointerEventData evaentData)
    {
        if (selectedIndex == -1)
        {
            if (!isDiscardSlot && fishInventory.GetItem(slotIndex) != null)
            {
                selectedIndex = slotIndex;
                CursorFollower.Instance.SetIcon(fishImage.sprite);
                fishImage.color = new Color(1, 1, 1, 0);
            }
        }
        else
        {
            if (isDiscardSlot)
            {
                fishInventory.RemoveAt(selectedIndex);
            }
            else
            {
                fishInventory.SwapSlots(selectedIndex, slotIndex);
            }
            selectedIndex = -1;
            CursorFollower.Instance.SetIcon(null);
        }
    }

    public void RefreshSlot()
    {
        if (isDiscardSlot) return;

        if (slotIndex == selectedIndex)
        {
            fishImage.sprite = null;
            fishImage.color = new Color(1, 1, 1, 0);
            return;
        }

        currentFish = fishInventory.GetItem(slotIndex);

        if (currentFish == null)
        {
            fishImage.sprite = null;
            fishImage.color = new Color(1, 1, 1, 0);
            return;
        }

        fishImage.sprite = currentFish.item_sprite;
        fishImage.color = Color.white;
    }

}