namespace Core.Main.Timers
{
    public struct TimerArgs
    {
        public ITimer Timer { get; }
        public float Period { get; }
        public float Value { get; }

        public TimerArgs(ITimer timer, float value, float period)
        {
            Timer = timer;
            Period = period;
            Value = value;
        }
    }
}