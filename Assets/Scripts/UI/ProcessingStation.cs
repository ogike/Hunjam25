using System.Collections.Generic;
using Model;
using UnityEngine;

public class ProcessingStation : MonoBehaviour
{
    private Transform _trans;

    private List<InventorySlot> _slots;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _slots = new List<InventorySlot>();
        foreach (InventorySlot slot in transform.GetComponentsInChildren<InventorySlot>())
        {
            _slots.Add(slot);
            slot.RegisterOwnerStation(this);
        }
    }

    public void ContaminateCheck()
    {
        //this shit ugly.... idc
        foreach (InventorySlot slot in _slots)
        {
            InventoryItem item = slot.GetInventoryItem();
            if(!item) continue;
            foreach (InventorySlot otherSlot in _slots)
            {
                if(slot == otherSlot) continue;
                InventoryItem otherItem = otherSlot.GetInventoryItem();
                if(!otherItem) continue;
                item.AddContamination(otherItem);
            }
        }
    }

    public void Cook()
    {
        bool success = false;
        
        foreach (InventorySlot slot in _slots)
        {
            InventoryItem inventoryItem = slot.GetInventoryItem();
            if(!inventoryItem){
                Debug.Log("Cooking with empty slot");
                continue; //error handling inside GetInventoryItem()
            }

            FoodItem foodItem = inventoryItem.foodItem;
            FoodItem result = foodItem.cookResult;
            
            if (result)
            {
                inventoryItem.InitializeItem(result);
                success = true;
            }
            else
            {
                Debug.Log($"Poof, this shit {foodItem.name} didnt cook man");
            }
        }

        if (success)
        {
            //Cook succesful sound
        }
    }

    public void MixingStation()
    {
        if (_slots.Count != 2)
        {
            Debug.LogError($"Attempting to mix with {_slots.Count} slots, should be 2");
            return;
        }

        InventoryItem item1 = _slots[0].GetInventoryItem();
        InventoryItem item2 = _slots[1].GetInventoryItem();
        if (!item1 || !item2)
        {
            Debug.Log("Not both slots have items to mix");
            return;
        }
        //TODO: checking if the items mix, what do they mix into
    }
}
