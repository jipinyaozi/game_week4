using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint activeCheckpoint; 
    public bool isActive = false; 
    public AudioSource sound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            sound.Play();
            Debug.Log("Checkpoint triggered");
            isActive = true; 
            activeCheckpoint = this; 
        }
    }
}
