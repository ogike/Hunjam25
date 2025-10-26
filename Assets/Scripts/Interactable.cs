using System;
using Dialogue;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject uiPanel;

    void Awake()
    {
        if (uiPanel == null)
        {
            Debug.LogWarning("No uiPanel set for this interactable!");
        }
    }

    private void OnMouseDown()
    {
        if(DialogueManager.Instance.dialogueIsPlaying) return;
        
        PanelsController.Instance.TogglePanel(uiPanel);
    }
}
