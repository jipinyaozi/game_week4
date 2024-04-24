using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelselect : MonoBehaviour
{
    // Method to handle the start button click event
    public void Lvl1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Lvl2()
    {
        SceneManager.LoadScene("Level 2");
    } 
    public void Lvl3()
    {
        SceneManager.LoadScene("Level 3");
    } 
    public void Lvl4()
    {
        SceneManager.LoadScene("Level 4");
    } 
}