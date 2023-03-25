
namespace BGCore.Core
{
    public class RwProperty<T> : DiProperty<T>
    {
        public T RwValue
        {
            get => Value;
            set => Value = value;
        }

        public RwProperty() : base() {}
        public RwProperty(T initValue) : base(initValue){}
    }
}