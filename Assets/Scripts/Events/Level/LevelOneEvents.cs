using System;

namespace Scripts.Events.Level
{
    public static class LevelOneEvents
    {
        public static event Action<string> OnPartFinished;
        public static event Action<string> OnCheckpointLoaded;

        public static void InvokePartFinished(string level)
            => OnPartFinished?.Invoke(level);

        public static void InvokeCheckpointLoaded(string level)
            => OnCheckpointLoaded?.Invoke(level);
    }
}