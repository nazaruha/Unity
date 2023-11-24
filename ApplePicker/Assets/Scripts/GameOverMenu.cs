using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public void ButtonRestart()
    {
        SceneManager.LoadSceneAsync("_Scene_0");
    }
    public void ButtonQuit()
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
