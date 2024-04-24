using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject[] buttonsToShow;
    private bool areButtonsVisible = false;

    void Start()
    {
        foreach (GameObject button in buttonsToShow)
        {
            button.SetActive(false);
        }
    }
    public void ToggleButtons()
    {
        areButtonsVisible = !areButtonsVisible;

        foreach (GameObject button in buttonsToShow)
        {
            button.SetActive(areButtonsVisible);
        }
    }
}
