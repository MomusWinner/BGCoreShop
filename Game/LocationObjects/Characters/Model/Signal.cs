namespace Game.Characters.Model
{
    public struct Signal<TKey, TValue> : ISignal
    {
        public object Key { get; }
        public object Value { get; }
        
        public Signal(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
    
    public interface ISignal
    {
        object Key { get; }
        object Value { get; }
    }
}