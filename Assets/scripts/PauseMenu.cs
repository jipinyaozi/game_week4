using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;  
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    public Button continueButton; 
    public Button quitButton;      

    void Start()
    {
        if (continueButton != null)
            continueButton.onClick.AddListener(Resume);
        else
            Debug.LogError("Continue button is not assigned!");

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
        else
            Debug.LogError("Quit button is not assigned!");
    }

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
        Debug.Log("Resume method called"); 
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            Debug.LogError("Pause menu UI GameObject is not found.");
        }
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
        Debug.Log("Quitting game...");
        Application.Quit();  
    }
}
