using UnityEngine;

namespace Scripts.Events.Level
{
    public class DuelResults : MonoBehaviour
    {
        public bool hasWon = false;

        private void OnDestroy()
        {
            hasWon = true;
        }
    }
}
