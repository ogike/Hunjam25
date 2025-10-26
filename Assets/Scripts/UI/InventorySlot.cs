using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public ProcessingStation ownerStation { get; private set; }

    public void RegisterOwnerStation(ProcessingStation owner)
    {
        ownerStation = owner;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }

    //called by InventoryItem during OnEndDrag
    public void AfterDrop()
    {
        if(ownerStation)
            ownerStation.ContaminateCheck();
    }

    public InventoryItem GetInventoryItem()
    {
        if (transform.childCount < 1)
        {
            return null;
        }
        Transform child = transform.GetChild(0);

        InventoryItem inventoryItem = child.GetComponent<InventoryItem>();
        if (!inventoryItem)
        {
            Debug.LogError(child.name + " is the first child of an InventorySlot without InventoryItem!");
        }

        return inventoryItem;
    }
}
