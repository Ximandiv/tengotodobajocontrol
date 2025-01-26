using System;

namespace Scripts.Events.Cutscenes
{
    public static class CutsceneEvents
    {
        public static event Action OnInitialFinished;

        public static void InvokeOnStart()
            => OnInitialFinished?.Invoke();
    }
}