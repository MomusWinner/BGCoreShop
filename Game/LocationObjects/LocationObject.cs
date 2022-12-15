using Core.Locations.Model;
using Core.ObjectsSystem;

namespace Game.LocationObjects
{
    public abstract class LocationObject : BaseDroppable
    {
        protected readonly Location parentLocation; 
        protected LocationObject(Location parent, string name) : base(name)
        {
            parentLocation = parent;
        }
    }
}