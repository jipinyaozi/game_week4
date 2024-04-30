using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumppad : MonoBehaviour
{
    [SerializeField] private float bounceForce = 100f;
    public AudioSource sound;
    private bool canBounce = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canBounce)
        {
            canBounce = false;  
            Rigidbody2D playerRb = GameObject.FindGameObjectWithTag("Door").GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
                sound.Play();
            }
        }
    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Door"))
        {
            canBounce = true;  
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Door"))
        {
            canBounce = false;  
        }
    }
}
