using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    [SerializeField] private GameObject startPanel;  
    [SerializeField] private GameObject pausePanel;
    public static event Action OnPause;
    public static event Action OnResume;

    private bool isGamePaused = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        startPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ContinueGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        OnPause?.Invoke();

        pausePanel.SetActive(true);
        Time.timeScale = 0f;  
        isGamePaused = true;
    }

    public void ContinueGame()
    {
        OnResume?.Invoke();

        pausePanel.SetActive(false);
        Time.timeScale = 1f;  
        isGamePaused = false;
    }

    public void StartGame()
    {
        
        startPanel.SetActive(false);
    }

    public void ToMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();  
    }
}
