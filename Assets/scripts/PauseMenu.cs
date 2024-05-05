using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;  
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    void Awake()
    {
        pauseMenuUI.SetActive(false);
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        if (pauseMenuUI != null)  
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;  
            isPaused = true;
        }
        else
        {
            Debug.LogError("Pause menu UI GameObject is missing.");
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
