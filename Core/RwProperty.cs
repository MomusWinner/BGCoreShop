
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
}