using System;
using System.Collections.Generic;
using System.Linq;
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
        
        HidePanels();
        playerInventoryPanel.SetActive(true);
    }

    public void HidePanels()
    {
        _panels.ForEach(panel => panel.SetActive(false));
    }

    public void ShowPanel(GameObject panel)
    {
        HidePanels();
        panel.SetActive(true);
    }
}
