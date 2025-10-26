using System;
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
        PanelsController.Instance.ShowPanel(uiPanel);
    }
}
