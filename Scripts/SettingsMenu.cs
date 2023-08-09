using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject SMenu;

    public bool isOptionPanelActive = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P key was pressed");
            isOptionPanelActive = true;

            if (isOptionPanelActive)
            {
                isOptionPanelActive = false;
                SMenu.SetActive(false);
            }
            else
            {
                SMenu.SetActive(true);
            }
        }
    }
}
