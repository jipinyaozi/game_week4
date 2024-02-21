using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject door; // Reference to the door GameObject
    public Rigidbody2D ballRigidbody; // Reference to the ball's Rigidbody2D component, now optional

    private bool isPressed = false; // Flag to track if the button is pressed
    private SpriteRenderer doorRenderer; // Reference to the door's SpriteRenderer component
    private Color originalColor; // Original color of the door sprite

    public float fadeSpeed = 1f; // Speed at which the door fades out
    public float ballForce = 10f; // Force to apply to the ball, if there is one

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
            if (door.activeSelf) // Check if the door is active to ensure the force is only applied once
            {
                StartCoroutine(FadeOutDoor());

                // Only apply force if ballRigidbody is assigned
                if (ballRigidbody != null)
                {
                    ballRigidbody.AddForce(-transform.right * ballForce, ForceMode2D.Impulse);
                }
            }
            door.SetActive(false);
        }
        else if (!isPressed)
        {
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
