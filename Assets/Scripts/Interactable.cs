using System;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject uiPanel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (uiPanel == null)
        {
            Debug.LogWarning("No uiPanel set for this interactable!");
        }
    }

    private void OnMouseDown()
    {
        PanelsController.Instance.ShowPanel(uiPanel);
    }
}
