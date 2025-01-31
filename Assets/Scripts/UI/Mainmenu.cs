using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    void Update()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 0) 
        {
            if (Input.anyKey)
            {
                SceneManager.LoadSceneAsync(1); 
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3) 
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadSceneAsync(1); 
            }
        }
    }

    
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2); 
    }

    
    public void ShowCredits()
    {
        SceneManager.LoadSceneAsync(3); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

