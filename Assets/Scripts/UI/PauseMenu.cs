using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button[] buttons; 
    private Animator[] buttonAnimators; 
    public static event Action OnPause;
    public static event Action OnResume;

    private bool isGamePaused = false;

    void Start()
    {
        startPanel.SetActive(true);
        pausePanel.SetActive(false);

        buttonAnimators = new Animator[buttons.Length];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttonAnimators[i] = buttons[i].GetComponent<Animator>();
        }
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

        ResetButtonsToNormal();

        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
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

    private void ResetButtonsToNormal()
    {
        
        foreach (var buttonAnimator in buttonAnimators)
        {
            buttonAnimator.ResetTrigger("Highlighted"); 
            buttonAnimator.SetTrigger("Normal"); 

            var buttonImage = buttonAnimator.gameObject.GetComponent<Image>();
            if (buttonImage != null)
            {
                buttonImage.color = Color.white; 
            }
        }
    }
}
