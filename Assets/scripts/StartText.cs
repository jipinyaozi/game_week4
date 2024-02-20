using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartText : MonoBehaviour
{
    public TextMeshProUGUI startText;

    private int deathCount = 0;
    private bool firstDeath = true;

    // Function to update the death text
    public void UpdateDeathText()
    {
        startText.text = "and again...";
    }

    // Function to handle player death
    public void PlayerDied()
    {
        deathCount++;

        // Check if it's the player's first death
        if (firstDeath)
        {
            firstDeath = false;
            // Update the text to display a different message
            startText.text = "Here we go again...";
        }
        else
        {
            UpdateDeathText(); // Update the death count
        }
    }
}