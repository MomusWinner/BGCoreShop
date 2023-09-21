
namespace Core
{
    public class RWProperty<T> : DiProperty<T>
    {
        public T RWValue
        {
            get => Value;
            set => Value = value;
        }

        public RWProperty() : base() {}
        public RWProperty(T initValue) : base(initValue){}
    }

    public class ROProperty<T> : DiProperty<T>
    {
        public T RValue => Value;
        
        public ROProperty() : base() {}
        public ROProperty(T initValue) : base(initValue){}

        public void SetValue(T value)
        {
            Value = value;
        }

    }
}