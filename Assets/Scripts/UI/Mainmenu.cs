using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class Mainmenu : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void Awake()
    {
        if (Time.timeScale <= 0f)
            Time.timeScale = 1f;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
            videoPlayer.Play();
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadSceneAsync(1);
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
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
        SceneManager.LoadSceneAsync(4);
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        StartCoroutine(VideoEndTransition());
    }

    private IEnumerator VideoEndTransition()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

