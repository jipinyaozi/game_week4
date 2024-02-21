using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string playerTag = "Player"; // Tag of the player GameObject


    public GameObject player; // Reference to the player GameObject
    public Transform playerpos;

    public float smoothSpeed = 0.125f; // How quickly the camera follows the player
    public Vector3 offset;

    void Start()
    {
        // Find the player GameObject using the tag
        player = GameObject.FindGameObjectWithTag(playerTag);
        playerpos = GameObject.FindGameObjectWithTag(playerTag)?.transform;

        if (playerpos.name == "Head")
        {
            playerpos = playerpos.transform;
        }
    }

    void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        playerpos = GameObject.FindGameObjectWithTag(playerTag)?.transform;


        if(player == null)
        {
            StartCoroutine(wait());
        }
        else{
                Vector3 desiredPosition = playerpos.position + offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                transform.position = smoothedPosition;
                transform.position = playerpos.position;
        }



    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
    }
}


