using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    // Name of the scene to transition to
    [SerializeField] private string nextSceneName;

    // Function to transition to the next scene
    public void TransitionToNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    // Trigger the transition when the player enters the trigger collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Main Player"))
        {
            TransitionToNextScene();
        }
    }
}
