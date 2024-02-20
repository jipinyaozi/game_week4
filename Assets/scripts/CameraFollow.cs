using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player GameObject

    private Transform player; // Reference to the player GameObject

    public float smoothSpeed = 0.125f; // How quickly the camera follows the player
    public Vector3 offset;

    void Start()
    {
        // Find the player GameObject using the tag
        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;

        if (player.name == "Body")
        {
            player = player.transform;
        }
    }

    void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;

        if (player.name == "Body")
        {
            player = player.transform;
        }
 

        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            StartCoroutine(wait());
            transform.position = smoothedPosition;
            // Update the position of the empty GameObject to follow the player
            transform.position = player.position;
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
    }
}
