using System;

namespace Scripts.Events.Cutscenes
{
    public static class CutsceneEvents
    {
        public static event Action OnStart;

        public static void InvokeOnStart()
            => OnStart?.Invoke();
    }
}