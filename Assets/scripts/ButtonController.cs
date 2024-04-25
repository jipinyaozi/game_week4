using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject door; 
    private bool isPressed = false;
    private SpriteRenderer doorRenderer; 
    private Color originalColor; 

    public float fadeSpeed = 1f; 

    void Start()
    {
        doorRenderer = door.GetComponent<SpriteRenderer>();
        originalColor = doorRenderer.color;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        isPressed = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isPressed = false;
    }

    void Update()
    {
        // Fade out the door while the player is on the button
        if (isPressed)
        {
            if (door.activeSelf) // Check if the door is active to ensure the force is only applied once
            {
                StartCoroutine(FadeOutDoor());
            }
        }
    }

    IEnumerator FadeOutDoor()
    {
        // Gradually reduce the alpha (transparency) of the door sprite
        while (doorRenderer.color.a > 0)
        {
            Color newColor = doorRenderer.color;
            newColor.a -= fadeSpeed * Time.deltaTime;
            doorRenderer.color = newColor;
            yield return null;
        }

        door.SetActive(false); // Deactivate the door
        yield return new WaitForSeconds(0.5f); // Wait

        if (isPressed)
        {
            StartCoroutine(FadeOutDoor());
        } else if (!isPressed) {
            doorRenderer.color = originalColor; // Reset the door color
            door.SetActive(true); // Reactivate the door
        }
    }
}
