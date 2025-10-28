using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameManager instance;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
    }
    void Start()
    {
       
        
        
    }

   


    void Update()
    {
        OnSceneLoaded();
    }

    private void OnSceneLoaded()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else if (SceneManager.GetActiveScene().name == "Stack")
        {
            Screen.SetResolution(1080, 1920, true);
        }
    }
}
