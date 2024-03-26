using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door")) 
        {
            Debug.Log("Checkpoint triggered");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var player = GameObject.FindGameObjectWithTag("Door");
            if (player != null)
            {
                float posX = player.transform.position.x;
                float posY = player.transform.position.y;
                PlayerPrefs.SetFloat("SavedPositionX", posX);
                PlayerPrefs.SetFloat("SavedPositionY", posY);
                PlayerPrefs.Save();
                Debug.Log($"Game saved at checkpoint: {posX}, {posY}");
            }
        }

    }
}
