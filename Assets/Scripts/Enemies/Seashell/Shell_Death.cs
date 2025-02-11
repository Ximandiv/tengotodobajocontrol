using System.Collections;
using UnityEngine;

namespace Scripts.Enemies.Seashell
{
    public class Shell_Death : MonoBehaviour
    {
        private Animator animator;
        private bool hasStarted = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void StartSequence()
        {
            if (hasStarted) return;
            hasStarted = true;

            animator.SetBool("isDead", true);
            StartCoroutine(waitAnimDead());
        }

        private IEnumerator waitAnimDead()
        {
            yield return new WaitForSeconds(2.25f);
            Destroy(transform.parent.gameObject);
        }
    }
}
