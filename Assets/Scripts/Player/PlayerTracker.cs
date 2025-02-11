using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public static PlayerTracker Instance { get; private set; }
    public Vector3 PlayerPosition { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerPosition = player.transform.position;
        }
    }
}