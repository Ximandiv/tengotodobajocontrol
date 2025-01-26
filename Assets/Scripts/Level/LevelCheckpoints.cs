using Scripts.Events.Level;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCheckpoints : MonoBehaviour
{
    public static Transform Instance;

    [SerializeField]
    private List<Transform> checkpoints = new();

    [SerializeField]
    private Transform lastCheckpoint;

    [SerializeField]
    private int currentIndex = 0;
    private string lastPartPassed = "One";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = transform;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        lastCheckpoint = transform;

        SceneManager.sceneLoaded += loadLastCheckpoint;

        LevelOneEvents.OnPartOneFinished += saveLastCheckpoint;
        LevelOneEvents.OnPartTwoFinished += saveLastCheckpoint;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= loadLastCheckpoint;

        LevelOneEvents.OnPartOneFinished -= saveLastCheckpoint;

        lastCheckpoint = null;
    }

    private void saveLastCheckpoint()
    {
        lastCheckpoint = checkpoints[currentIndex];
        currentIndex++;

        switch (currentIndex)
        {
            case 1:
                LevelOneEvents.OnPartOneFinished -= saveLastCheckpoint;
                lastPartPassed = "One";
                break;
            case 2:
                LevelOneEvents.OnPartTwoFinished -= saveLastCheckpoint;
                lastPartPassed = "Two";
                break;
            case 3:
                lastPartPassed = "Three";
                break;
            case 4:
                lastPartPassed = "Four";
                break;
            default:
                break;
        }
    }

    private void loadLastCheckpoint(Scene scene, LoadSceneMode mode)
    {
        if (lastCheckpoint == transform) return;

        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckpoint.position;

        switch (currentIndex)
        {
            case 1:
                LevelOneEvents.InvokePartOneFinished();
                break;
            case 2:
                LevelOneEvents.InvokePartTwoFinished();
                break;
            case 3:
                LevelOneEvents.InvokePartThreeFinished();
                break;
            case 4:
                LevelOneEvents.InvokePartFourFinished();
                break;
            default:
                break;
        }

        LevelOneEvents.InvokeCheckpointLoaded(lastPartPassed);
    }
}
