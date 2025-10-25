using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;

    public FoodItem foodItem;
    [HideInInspector] public Transform parentAfterDrag;

    public void Start()
    {
        if (foodItem == null)
        {
            Debug.LogWarning(transform.name + "oesnt have foodItem set!");
            return;
        }
        InitializeItem(foodItem);
    }

    public void InitializeItem(FoodItem item)
    {
        foodItem = item;
        image.sprite = foodItem.image;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}
