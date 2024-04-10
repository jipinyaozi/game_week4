using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class editorbutton : MonoBehaviour
{
    // Method to handle the start button click event
    public void Gotoedit()
    {
        // Load the scene with the specified name
        SceneManager.LoadScene("level editor");
    }
}