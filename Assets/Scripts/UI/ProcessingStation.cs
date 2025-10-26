using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using SaintsField;
using UnityEngine;

public class ProcessingStation : MonoBehaviour
{
    public enum ProcessingType
    {
        Cook,
        Bake,
        Chop,
        Mix
    }

    [Tooltip("Only needed for mixing station")]
    [Expandable] public FoodItemRegistry foodItemRegistry;

    public bool isStoreroom;

    private List<InventorySlot> _slots;

    void Start()
    {
        _slots = new List<InventorySlot>();
        List<GameObject> toDelete = new List<GameObject>();
        foreach (InventorySlot slot in transform.GetComponentsInChildren<InventorySlot>())
        {
            if (isStoreroom && slot.transform.childCount == 0)
            {
                toDelete.Add(slot.gameObject);
                continue;
            }
            _slots.Add(slot);
            slot.RegisterOwnerStation(this);
        }

        if (isStoreroom)
        {
            toDelete.ForEach(Destroy);
        }

        ContaminateCheck();
    }

    public void ContaminateCheck()
    {
        if(isStoreroom) return;
        
        //this shit ugly.... idc
        foreach (InventorySlot slot in _slots)
        {
            InventoryItem item = slot.GetInventoryItem();
            if (!item) continue;
            foreach (InventorySlot otherSlot in _slots)
            {
                if (slot == otherSlot) continue;
                InventoryItem otherItem = otherSlot.GetInventoryItem();
                if (!otherItem) continue;
                item.AddContamination(otherItem);
            }
        }
    }

    public void Cook() => SimpleProcessing(ProcessingType.Cook);

    public void Bake() => SimpleProcessing(ProcessingType.Bake);

    public void Chop() => SimpleProcessing(ProcessingType.Chop);

    public void SimpleProcessing(ProcessingType process)
    {
        if (isStoreroom)
        {
            Debug.LogError("Storeroom cant do processing!");
            return;
        }
        
        bool success = false;

        foreach (InventorySlot slot in _slots)
        {
            InventoryItem inventoryItem = slot.GetInventoryItem();
            if (!inventoryItem)
            {
                Debug.Log("Cooking with empty slot");
                continue; //error handling inside GetInventoryItem()
            }

            FoodItem foodItem = inventoryItem.foodItem;
            FoodItem result;

            switch (process)
            {
                case ProcessingType.Cook:
                    result = foodItem.cookResult;
                    break;
                case ProcessingType.Bake:
                    result = foodItem.bakeResult;
                    break;
                case ProcessingType.Chop:
                    result = foodItem.chopResult;
                    break;
                case ProcessingType.Mix:
                    //overflow
                default:
                    Debug.LogError("Wrong processing type for simple processing!");
                    return;
            }
            
            if (result)
            {
                inventoryItem.InitializeItem(result);
                success = true;
            }
            else
            {
                Debug.Log($"Poof, this shit {foodItem.name} didnt {process.ToString()} man");
            }
        }

        if (success)
        {
            //Cook succesful sound
        }
    }

    public void MixingStation()
    {
        if (isStoreroom)
        {
            Debug.LogError("Storeroom cant do processing!");
            return;
        }
        
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



        if (item1 == item2)
        {
            Debug.Log("Cannot mix two of the same item");
            return;
        }

        if (string.Compare(item1.foodItem.name, item2.foodItem.name) > 0)
        {
            Debug.Log($"To make sure items are in alphabetical order, I'm swapping item1({item1.foodItem.name}) and item2({item2.foodItem.name}) references");
            (item1, item2) = (item2, item1);
        }

#nullable enable
        FoodItem? result = (item1.foodItem.name, item2.foodItem.name) switch
#nullable disable
        {
            ("GlutenCookedIsPasta", "MeatChoppedCooked") => foodItemRegistry.bolognese,
            ("Cheese", "Gluten") => foodItemRegistry.breadedCheeseRaw,
            ("RiceChoppedCookedIsRicepaper", "TobaccoLeavesChoppedBakedIsDriedTobacco") => foodItemRegistry.cigarettes,
            ("GlutenBakedIsBuns", "MeatCooked") => foodItemRegistry.hotdog,
            ("Cheese", "GlutenCookedIsPasta") => foodItemRegistry.macNCheese,
            ("MeatChoped", "PotatoChoppedCooked") => foodItemRegistry.rakottKrumpliRaw,
            ("MeatChopped", "CookedRice") => foodItemRegistry.rizseshus,
            ("FishyChopped", "CookedRice") => foodItemRegistry.sushi,
            ("VegetablesChopped", "Rice") => foodItemRegistry.sushiVegan,
            ("Gluten", "TacoContentCooked") => foodItemRegistry.tacoFinished,
            ("Gluten", "Meat") => foodItemRegistry.wienerScnitzelRaw,

            _ => null
        };

        if(Mix(item1, item2, result))
        {
            //Mix succesful sound
        }

    }

#nullable enable
    private bool Mix(InventoryItem i1, InventoryItem i2, FoodItem? result)
#nullable disable
    {
        if (!result)
        {
            Debug.LogWarning($"Mixing would result in null FoodItem!!!!");
            return false;
        }

        Debug.Log($"Making {result.name} from {i1.foodItem.name} and {i2.foodItem.name} and adding contaminants [{i1.GetContaminations()}, {i2.GetContaminations()}].");
        i1.AddContamination(i2);
        i1.InitializeItem(result);
        GameObject.Destroy(i2.gameObject);

        return true;
    }
}
