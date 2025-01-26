using Scripts.Events.Player;
using UnityEngine;

namespace Scripts.Audio
{
    public class AudioManager : MonoBehaviour
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
}
