using UnityEngine;

namespace Game.Characters.Control
{
    public abstract class ControllableSetting : ScriptableObject
    {
    }

    [CreateAssetMenu(fileName = "")]
    public class PlayerControllableSetting : ControllableSetting
    {
        
    }
}