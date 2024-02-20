using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject door; // Reference to the door GameObject

    private bool isPressed = false; // Flag to track if the button is pressed
    private SpriteRenderer doorRenderer; // Reference to the door's SpriteRenderer component
    private Color originalColor; // Original color of the door sprite

    public float fadeSpeed = 1f; // Speed at which the door fades out

    void Start()
    {
        // Get the door's SpriteRenderer component and store its original color
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
            StartCoroutine(FadeOutDoor());
            door.SetActive(false);
        } else if (isPressed == false) {
            doorRenderer.color = originalColor;
            door.SetActive(true);
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
    }
}