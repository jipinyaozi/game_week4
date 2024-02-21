using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    void Update()
    {
        // Retry level 1 if 1 is pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Tutorial");
        }
        // Retry level 2 if 2 is pressed
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Main Level");
        }
    }
}
