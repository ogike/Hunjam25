using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Model;
using SaintsField;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image _image;

    private FoodType _contaminations;
    
    [Expandable] public FoodItem foodItem;
    [HideInInspector] public Transform parentAfterDrag;
    private Transform _parentBeforeDrag;

    private bool _hovering;

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
        _parentBeforeDrag = transform.parent;
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

        InventorySlot oldSlot = _parentBeforeDrag.GetComponent<InventorySlot>();

        InventorySlot newSlot = parentAfterDrag.GetComponent<InventorySlot>();

        if (oldSlot && oldSlot.ownerStation != null && oldSlot != newSlot)
        {
            if(oldSlot.ownerStation.isStoreroom)
                Destroy(oldSlot.gameObject);
        }
        
        if (newSlot)
        {
            newSlot.AfterDrop();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _hovering = true;
        string itemName = foodItem.displayName;
        if (itemName.Length == 0)
            itemName = foodItem.name;

        itemName += $"\nType: {foodItem.baseType}\n";
        
        PanelsController.Instance.SetHoverVisibility(true);
        PanelsController.Instance.SetHoverText(itemName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!_hovering) return; //dont know if this actually catches shit
        _hovering = false;
        PanelsController.Instance.SetHoverVisibility(false);
    }
}
