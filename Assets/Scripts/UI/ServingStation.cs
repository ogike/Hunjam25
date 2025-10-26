using System;
using Dialogue;
using Model;
using UnityEngine;
using UnityEngine.Serialization;

public class ServingStation : MonoBehaviour
{
    [SerializeField] private InventorySlot slot;
    public GameObject uiPanel;
    public int number;

    private Preferences _curPreferences;

    public void Start()
    {
        if(slot == null)
            Debug.LogWarning($"Slot not given for {transform.name}");
        if (uiPanel == null)
            Debug.LogWarning("No uiPanel set for this interactable!");
    }

    public void Initialize(string character)
    {
        switch (character)
        {
            case DialogueManager.NAVI_STRING_TAG:
                _curPreferences = PreferenceRegistry.Instance.naviPrefs;
                break;
            case DialogueManager.OFFI_STRING_TAG:
                _curPreferences = PreferenceRegistry.Instance.offiPrefs;
                break;
            case DialogueManager.ENGI_STRING_TAG:
                _curPreferences = PreferenceRegistry.Instance.engiPrefs;
                break;
            default:
                Debug.LogError("Wrong tag received by ServingStation: " + character);
                break;
        }
    }

    public void Serve()
    {
        InventoryItem inventoryItem = slot.GetInventoryItem();
        if (!inventoryItem)
        {
            Debug.Log("Serving with empty slot");
            return;
        }

        FoodItem food = inventoryItem.foodItem;

        if ((inventoryItem.GetContaminations() & _curPreferences.dislikes) != FoodType.Neutral)
        {
            DialogueManager.Instance.SetVariableString(_curPreferences.prevFoodTag, "bad");
        }
        else if ((food.baseType & _curPreferences.likes) != FoodType.Neutral)
        {
            DialogueManager.Instance.SetVariableString(_curPreferences.prevFoodTag, "good");
        }
        else
        {
            DialogueManager.Instance.SetVariableString(_curPreferences.prevFoodTag, "neutral");
        }
        
        if(number == 1)
            DialogueTrigger.Instance.Food1Done();
        else
            DialogueTrigger.Instance.Food2Done();

        Destroy(inventoryItem.gameObject);
    }
    
    private void OnMouseDown()
    {
        if(DialogueManager.Instance.dialogueIsPlaying) return;

        PanelsController.Instance.TogglePanel(uiPanel);
    }
}
