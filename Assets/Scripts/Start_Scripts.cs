using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Scripts : MonoBehaviour
{
    void Start() { }

    void Update()
    {
        
    }

    public void startScene()
    {
        SceneManager.LoadScene("Level1Scene");
        Time.timeScale = 1;
    }
}
