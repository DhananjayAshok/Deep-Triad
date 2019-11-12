using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static int AIPlayers = 0;
    public Dropdown dropdown;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAIPlayers() {
        int newAIPlayers = dropdown.value;
        AIPlayers = newAIPlayers;
        //Debug.Log(AIPlayers);
    }

    public void Play() {
        SceneManager.LoadSceneAsync("Game");
    
    }

    public void Exit() {
        Application.Quit();
    }
}
