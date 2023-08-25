using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    //Reload the same scene
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    //Change the scene to a known one
    public void ChangeSceneByName(string name)
    {
        if (name != null) {
            SceneManager.LoadScene(name);
        }
    }
}
