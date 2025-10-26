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

    private FoodType _contaminations;
    
    [Expandable] public FoodItem foodItem;
    [HideInInspector] public Transform parentAfterDrag;

    public void Start()
    {
        if (foodItem == null)
        {
            Debug.LogWarning(transform.name + " doesnt have foodItem set!");
            return;
        }

        _image = GetComponent<Image>();
        _contaminations = FoodType.Neutral;
        InitializeItem(foodItem);
    }
    
    public void AddContamination(FoodType contamination)
    {
        Debug.Log($"Adding contamination to {name}: had {_contaminations}, adding {contamination}");
        _contaminations |= contamination;
    }

    public void AddContamination(InventoryItem item)
    {
        this._contaminations |= item._contaminations;
        Debug.Log($"Adding contamination to {name}: had {_contaminations}, adding {item._contaminations}");
    }

    public FoodType GetContaminations() => _contaminations;

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
