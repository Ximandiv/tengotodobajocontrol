using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool isPaused;

    public void Start()
    {
        setChildren(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                Continue();
            }
            else
            {
                Pause();
            }
   
        }
    }

    private void Pause()
    {
        setChildren(true);

        Time.timeScale = 0f;
        isPaused = true;
    
    }

    private void Continue()
    {
        setChildren(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    private void setChildren(bool activeStatus)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(activeStatus);
        }
    }

    public void ToMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueButton()
    {
        setChildren(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

}
