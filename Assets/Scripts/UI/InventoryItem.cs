using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Model;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;

    private List<FoodType> _contaminations;
    
    public FoodItem foodItem;
    [HideInInspector] public Transform parentAfterDrag;

    public void Start()
    {
        if (foodItem == null)
        {
            Debug.LogWarning(transform.name + " doesnt have foodItem set!");
            return;
        }

        _contaminations = new List<FoodType>();
        InitializeItem(foodItem);
    }
    
    public void AddContamination(FoodType contamination)
    {
        _contaminations.Add(contamination);
    }

    public List<FoodType> GetContaminations() => _contaminations;

    public void InitializeItem(FoodItem item)
    {
        foodItem = item;
        image.sprite = foodItem.image;
        AddContamination(item.baseType);
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
