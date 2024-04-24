using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Method to handle the start button click event
    public void StartGame()
    {
        // Load the scene with the specified name
        SceneManager.LoadScene("Level 1");
    }
}