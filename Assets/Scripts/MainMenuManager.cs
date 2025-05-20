using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
        //TODO THIS LAST LINE OF CODE ONLY WORKS FOR UNITY EDITOR AND SHOULD BE REMOVE BEFORE THE FINAL BUILD OF THE PROJECT
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
