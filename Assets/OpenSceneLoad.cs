using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSceneLoad : MonoBehaviour
{
    private bool isRunning = false;

    private void Update()
    {
        if (!isRunning)
            StartCoroutine(WaitBeforeChange());

        if(Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            SceneManager.LoadSceneAsync(1);
        }
    }

    private IEnumerator WaitBeforeChange()
    {
        isRunning = true;
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(1);
    }
}
