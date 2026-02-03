using TMPro;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemPriceText;

    private UIStoreSlot currentStoreSlot;
    private FishInventory playerInventory;

    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>().Inventory;
    }

    private void OnEnable()
    {
        UIStoreSlot.OnStoreSlotSelected += DisplayItemDetails;
    }

    private void OnDisable()
    {
        UIStoreSlot.OnStoreSlotSelected -= DisplayItemDetails;
    }

    public void DisplayItemDetails(UIStoreSlot storeSlot, ItemSO item)
    {
        currentStoreSlot = storeSlot;

        itemNameText.text = item.item_name;
        itemDescriptionText.text = item.ItemDescription;
        itemPriceText.text = BuildPriceText(storeSlot);
    }

    public void BuyItem()
    {
        if (currentStoreSlot == null) return;
        if (currentStoreSlot.soldOut) return;

        if (!CanAfford(playerInventory, currentStoreSlot))
        {
            Debug.Log("Not enough fish!");
            return;
        }

        PayCost(playerInventory, currentStoreSlot);

        bool added = playerInventory.AddItem(currentStoreSlot.shopItem);

        if (added)
        {
            currentStoreSlot.soldOut = true;
            currentStoreSlot.iconImage.color = Color.gray;
        }
    }


    bool CanAfford(FishInventory inv, UIStoreSlot slot)
    {
        if (slot.acceptAnyFish)
        {
            return CountTotalFish(inv) >= slot.anyFishAmount;
        }

        foreach (var cost in slot.costs)
        {
            if (CountItem(inv, cost.fish) < cost.amount)
                return false;
        }

        return true;
    }

    void PayCost(FishInventory inv, UIStoreSlot slot)
    {
        if (slot.acceptAnyFish)
        {
            RemoveAnyFish(inv, slot.anyFishAmount);
            return;
        }

        foreach (var cost in slot.costs)
        {
            RemoveItemAmount(inv, cost.fish, cost.amount);
        }
    }
    

    int CountItem(FishInventory inv, ItemSO item)
    {
        int count = 0;

        for (int i = 0; i < 20; i++)
        {
            if (inv.GetItem(i) == item)
                count++;
        }

        return count;
    }

    int CountTotalFish(FishInventory inv)
    {
        int count = 0;

        for (int i = 0; i < 20; i++)
        {
            if (inv.GetItem(i) != null)
                count++;
        }

        return count;
    }

    void RemoveItemAmount(FishInventory inv, ItemSO item, int amount)
    {
        for (int i = 0; i < 20 && amount > 0; i++)
        {
            if (inv.GetItem(i) == item)
            {
                inv.RemoveAt(i);
                amount--;
            }
        }
    }

    void RemoveAnyFish(FishInventory inv, int amount)
    {
        for (int i = 0; i < 20 && amount > 0; i++)
        {
            if (inv.GetItem(i) != null)
            {
                inv.RemoveAt(i);
                amount--;
            }
        }
    }

    string BuildPriceText(UIStoreSlot slot)
    {
        if (slot.acceptAnyFish)
        {
            return $"Cost:\n{slot.anyFishAmount}x Any Fish";
        }

        string text = "Cost:\n";

        foreach (var cost in slot.costs)
        {
            text += $"{cost.amount}x {cost.fish.item_name}\n";
        }

        return text;
    }
}
