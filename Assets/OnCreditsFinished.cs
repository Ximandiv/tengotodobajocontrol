using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCreditsFinished : MonoBehaviour
{
    [SerializeField]
    private int totalCreditsTime = 20;
    private bool isCounting = false;

    private void Update()
    {
        if (!isCounting && totalCreditsTime > 0)
            StartCoroutine(countdown());
        else if (totalCreditsTime == 0)
            SceneManager.LoadSceneAsync(1);

        if (Input.GetKeyDown(KeyCode.Escape)
            || Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            totalCreditsTime = 20;
            isCounting = false;
            SceneManager.LoadSceneAsync(1);
        }
    }

    private IEnumerator countdown()
    {
        isCounting = true;
        yield return new WaitForSeconds(1);
        totalCreditsTime--;
        isCounting = false;
    }
}
