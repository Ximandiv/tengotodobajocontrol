using Scripts.Events.Cutscenes;
using UnityEngine;

namespace Scripts
{
    public class ActivateOnCutscene : MonoBehaviour
    {
        [SerializeField]
        private MonoBehaviour toActivate;

        private void Awake()
        {
            CutsceneEvents.OnInitialFinished += onActivate;
        }

        private void OnDestroy()
        {
            CutsceneEvents.OnInitialFinished -= onActivate;
        }

        private void onActivate()
            => toActivate.enabled = true;
    }
}