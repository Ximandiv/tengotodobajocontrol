using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static Transform Instance;

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
    }
}
