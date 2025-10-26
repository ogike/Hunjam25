using System;
using Dialogue;
using Model;
using UnityEngine;
using UnityEngine.Serialization;

public class ServingStation : MonoBehaviour
{
    [SerializeField] private InventorySlot slot;
    public GameObject uiPanel;

    public Preference preference;

    private bool _isBlocked = false;

    public void Start()
    {
        if(slot == null)
            Debug.LogWarning($"Slot not given for {transform.name}");
        if (uiPanel == null)
            Debug.LogWarning("No uiPanel set for this interactable!");
    }

    public void Serve()
    {
        if(_isBlocked) return;
        
        InventoryItem inventoryItem = slot.GetInventoryItem();
        if (!inventoryItem)
        {
            Debug.Log("Serving with empty slot");
            return;
        }

        FoodItem food = inventoryItem.foodItem;

        if ((inventoryItem.GetContaminations() & preference.dislikes) != FoodType.Neutral)
        {
            DialogueManager.Instance.SetVariableString(preference.prevFoodTag, "bad");
        }
        else if ((food.baseType & preference.likes) != FoodType.Neutral)
        {
            DialogueManager.Instance.SetVariableString(preference.prevFoodTag, "good");
        }
        else
        {
            DialogueManager.Instance.SetVariableString(preference.prevFoodTag, "neutral");
        }

        DialogueTrigger.Instance.FoodDone(preference);

        SetIsBlocked(true);
    }


    public void SetIsBlocked(bool value)
    {
        _isBlocked = value;
        uiPanel.SetActive(!value);
    }
    private void OnMouseDown()
    {
        if(DialogueManager.Instance.dialogueIsPlaying || _isBlocked) return;

        PanelsController.Instance.TogglePanel(uiPanel);
    }
}
