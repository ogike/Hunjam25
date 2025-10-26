using System;
using System.Collections.Generic;
using System.Linq;
using Dialogue;
using UnityEngine;

public class PanelsController : MonoBehaviour
{
    public static PanelsController Instance { get; private set; }
    
    private List<GameObject> _panels;
    public GameObject playerInventoryPanel;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple PanelsControllers in scene!");
            return;
        }

        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        List<ProcessingStation> stations = transform.GetComponentsInChildren<ProcessingStation>().ToList();
        _panels = new List<GameObject>();
        stations.ForEach(station => _panels.Add(station.gameObject));
        
        //inventory is handled separately
        if (_panels.Contains(playerInventoryPanel))
            _panels.Remove(playerInventoryPanel);

        //spaghetti woooooo
        _panels.Add(DialogueManager.Instance.servingStation1.uiPanel);
        _panels.Add(DialogueManager.Instance.servingStation2.uiPanel);
        
        HidePanels();
        playerInventoryPanel.SetActive(true);
    }

    public void HidePanels()
    {
        _panels.ForEach(panel => panel.SetActive(false));
    }

    public void TogglePanel(GameObject panel)
    {
        if (panel.activeInHierarchy)
        {
            panel.SetActive(false);
        }
        else
        {
            HidePanels();
            panel.SetActive(true);
        }
    }
}
