using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject PauseIt;
    public bool isPaused;


    // Update is called once per frame
    public void Start()
    {
        PauseIt.SetActive(false);
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
     PauseIt.gameObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    
    }

    private void Continue()
    {
        PauseIt.gameObject.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

    }
}
