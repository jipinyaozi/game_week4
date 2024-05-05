using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint activeCheckpoint; 
    public bool isActive = false; 
    public AudioSource sound;
    private bool soundplayed = false;
    public SpriteRenderer sprd;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            if (!isActive)
            {
            sound.Play();
            Debug.Log("Checkpoint triggered");
            isActive = true; 
            activeCheckpoint = this; 
            sprd.color = Color.green;
            }
        }
    }
}
