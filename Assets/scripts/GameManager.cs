using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (PlayerPrefs.GetInt("LoadSavedState", 0) == 1)
        {
            LoadSavedState();
        }
    }

    private void LoadSavedState()
    {
        float savedX = PlayerPrefs.GetFloat("SavedPositionX", 0);
        float savedY = PlayerPrefs.GetFloat("SavedPositionY", 0);

        var player = GameObject.FindGameObjectWithTag("Door");
        if (player != null)
        {
            player.transform.position = new Vector3(savedX, savedY, 0);
            PlayerPrefs.SetInt("LoadSavedState", 0);  
            PlayerPrefs.Save();
        }
    }
}
