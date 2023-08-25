using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;

    //Show and hide the death panel
    public void ToggleDeathPanel()
    {
        Time.timeScale = 0f;
        deathPanel.SetActive(!deathPanel.activeSelf);
    }
}
