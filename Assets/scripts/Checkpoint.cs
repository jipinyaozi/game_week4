using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint activeCheckpoint; 
    public bool isActive = false; 
    public AudioSource sound;
    private bool soundplayed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            if (activeCheckpoint != this)
            {
            sound.Play();
            Debug.Log("Checkpoint triggered");
            isActive = true; 
            activeCheckpoint = this; 
            }
        }
    }
}
