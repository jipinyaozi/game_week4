using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumppad : MonoBehaviour
{
    [SerializeField] private float bounce = 5f;
    public AudioSource sound;
    private bool PlayerJumped = false;

    private void OnCollisionEnter2D(Collision2D col) {

        if (col.gameObject.CompareTag("Door"))
        {
            if (Input.GetKeyDown(KeyCode.Space)) {
                PlayerJumped = true;
            }
        }

        if (PlayerJumped)
        {
            sound.Play();
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            PlayerJumped = false;
        } 
        
    }
    
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
    }

}
