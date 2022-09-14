using Game.Settings;
using UnityEngine;

namespace Game.Characters.Model
{
    public abstract class BaseCharacterSetting : ViewSetting
    {
        public float Speed => speed;
        public float Gravity => gravity;
        public float JumpHeight => jumpHeight;
        public float GroundDistance => groundDistance;
        public LayerMask GroundMask => groundMask;
        
        [SerializeField] private float speed = 12f;
        [SerializeField] private float gravity = -10f;
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float groundDistance = 0.4f;
        [SerializeField] private LayerMask groundMask;
    }
}