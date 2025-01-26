using Scripts.Events.Level;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Level
{
    public class KillAllPart : MonoBehaviour
    {
        [SerializeField]
        private string levelPart = "One";
        [SerializeField]
        private Transform invisibleWall;
        [SerializeField]
        private Transform partObject;
        [SerializeField]
        private List<Transform> enemiesToKill;

        public void InitializeContainer(string partPassed)
        {
            if (levelPart != partPassed) return;

            invisibleWall.GetComponent<BoxCollider2D>().enabled = true;
        }

        private void Awake()
        {
            foreach(Transform enemy in partObject)
            {
                enemiesToKill.Add(enemy);
            }
            LevelOneEvents.OnPartFinished += InitializeContainer;
        }

        private void OnDestroy()
        {
            LevelOneEvents.OnPartFinished -= InitializeContainer;
        }

        private void Update()
        {
            if (enemiesToKill.All(e => e != null)) return;

            enemiesToKill.Clear();

            Destroy(invisibleWall.gameObject);
            this.enabled = false;
        }
    }
}
