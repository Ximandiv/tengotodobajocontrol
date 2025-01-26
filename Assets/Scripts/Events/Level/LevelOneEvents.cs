using System;

namespace Scripts.Events.Level
{
    public static class LevelOneEvents
    {
        public static event Action OnPartOneFinished;
        public static event Action OnPartTwoFinished;
        public static event Action OnPartThreeFinished;
        public static event Action OnPartFourFinished;
        public static event Action<string> OnCheckpointLoaded;

        public static void InvokePartOneFinished()
            => OnPartOneFinished?.Invoke();
        public static void InvokePartTwoFinished()
            => OnPartTwoFinished?.Invoke();
        public static void InvokePartThreeFinished()
            => OnPartThreeFinished?.Invoke();
        public static void InvokePartFourFinished()
            => OnPartFourFinished?.Invoke();

        public static void InvokeCheckpointLoaded(string level)
            => OnCheckpointLoaded?.Invoke(level);
    }
}