using UnityEngine;
using UnityEngine.UI;
using Scripts.Player;

namespace Scripts.UI
{
    public class AttackUI : MonoBehaviour
    {
        [SerializeField] private Attack playerAttack;  
        [SerializeField] private Image bubbleCooldown;  

        private void Update()
        {
            
            if (playerAttack != null && bubbleCooldown != null)
            {
                
                bubbleCooldown.color = playerAttack.isAttacking ? new Color(0f, 0f, 0f, 0.5f) : Color.white;
            }
        }
    }
}

