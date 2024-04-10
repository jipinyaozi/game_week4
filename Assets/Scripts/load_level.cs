using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_level : MonoBehaviour
{
    // Start is called before the first frame update
    public void GotoLoad()
    {
        // Load the scene with the specified name
        SceneManager.LoadScene("load level");
    }
}
