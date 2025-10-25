using System.Collections.Generic;
using UnityEngine;

public class PanelsController : MonoBehaviour
{
    public List<GameObject> panels;
    public GameObject playerInventoryPanel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //inventory is handled separately
        if (panels.Contains(playerInventoryPanel))
            panels.Remove(playerInventoryPanel);
        
        HidePanels();
        playerInventoryPanel.SetActive(true);
    }

    public void HidePanels()
    {
        panels.ForEach(panel => panel.SetActive(false));
    }

    public void ShowPanel(GameObject panel)
    {
        HidePanels();
        panel.SetActive(true);
    }
}
