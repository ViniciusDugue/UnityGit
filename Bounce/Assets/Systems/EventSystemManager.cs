using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class EventSystemManager : MonoBehaviour

{
    private static EventSystemManager instance;
    void OnEnable()
    {

        // Check if an instance of this object already exists in the scene
        if (instance == null)
        {
            // If not, set this instance as the singleton instance and don't destroy it when loading new scenes
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            // If another instance already exists, destroy it and set this instance as the singleton instance
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
   
}