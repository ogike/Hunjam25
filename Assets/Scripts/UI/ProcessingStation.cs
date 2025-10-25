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
        }
    }

    public void Cook()
    {
        bool success = false;
        
        foreach (InventorySlot slot in _slots)
        {
            if (slot.transform.childCount < 1)
            {
                Debug.Log("Cooking with empty slot");
                continue;
            }
            Transform child = slot.transform.GetChild(0);

            InventoryItem inventoryItem = child.GetComponent<InventoryItem>();
            if (!inventoryItem)
            {
                Debug.LogError(child.name + " is the first child of an InventorySlot without InventoryItem!");
                continue;
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
                Debug.Log($"Poof, this shit {child.name} didnt cook man");
            }
        }

        if (success)
        {
            //Cook succesful sound
        }
    }

    public void MixingStation()
    {
        //
    }
}
