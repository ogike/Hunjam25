using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Model;
using SaintsField;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image _image;

    private List<FoodType> _contaminations;
    
    [Expandable] public FoodItem foodItem;
    [HideInInspector] public Transform parentAfterDrag;

    public void Start()
    {
        if (foodItem == null)
        {
            Debug.LogWarning(transform.name + " doesnt have foodItem set!");
            return;
        }

        _contaminations = new List<FoodType>();
        _image = GetComponent<Image>();
        InitializeItem(foodItem);
    }
    
    public void AddContamination(FoodType contamination)
    {
        if(_contaminations.Contains(contamination)) return;

        _contaminations.Add(contamination);
    }

    public void AddContamination(InventoryItem item)
    {
        foreach (FoodType contamination in item._contaminations)
        {
            this._contaminations.Add(contamination);
        }
    }

    public List<FoodType> GetContaminations() => _contaminations;

    public void InitializeItem(FoodItem item)
    {
        foodItem = item;
        if(foodItem.image)
            _image.sprite = foodItem.image;
        else
        {
            Debug.LogWarning("Image for food not set!");
        }
        AddContamination(item.baseType);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
        transform.SetParent(parentAfterDrag); //OnDrop will set ParentAfterDrag

        InventorySlot newSlot = parentAfterDrag.GetComponent<InventorySlot>();
        if (newSlot)
        {
            newSlot.AfterDrop();
        }
    }
}
