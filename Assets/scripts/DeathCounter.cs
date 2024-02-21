using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DeathCounter : MonoBehaviour
{
    public TextMeshProUGUI deathText;
    private int deathCount = 0;

    void Start()
    {
        // Initialize the death count text
        UpdateDeathText();
    }

    // Function to update the death text
    void UpdateDeathText()
    {
        deathText.text = "DEATHS: " + deathCount.ToString();
    }

    // Function to handle player death
    public void PlayerDied()
    {
        deathCount++;
        UpdateDeathText(); // Update the death count
    }
}
