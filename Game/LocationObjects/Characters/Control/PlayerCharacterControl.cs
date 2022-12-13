using Game.Characters.Model;
using GameLogic;
using UnityEngine;

namespace Game.Characters.Control
{
    public class PlayerCharacterControl : CharacterControl
    {
        private readonly PlayerCharacterSetting setting;
        private Animator animator;
        private float speed;

        public PlayerCharacterControl(ICharacter character, PlayerCharacterSetting setting) : base(character)
        {
            this.setting = setting;
        }

        public override void SetAlive()
        {
            base.SetAlive();
            animator = Character.PlayerRoot.GetComponentInChildren<Animator>();
        }
        
        public override void Action(ICommand command)
        {
            switch (command.Signal.Value)
            {
                case Vector3 vector3:
                    speed = Mathf.MoveTowards(speed, vector3.x * setting.animatorSpeed,Time.deltaTime * setting.changeAnimationSpeed);
                    SetAnimator(command.Signal.Key.ToString(), speed);
                    break;
                default:
                    return;
            }
        }
        
        private void SetAnimator<TValue>(string parameterName, TValue value)
        {
            switch (value)
            {
                case float valueFloat:
                    animator.SetFloat(parameterName, valueFloat);
                    break;
                case bool valueBool:
                    animator.SetBool(parameterName, valueBool);
                    break;
                case int valueInt:
                    animator.SetInteger(parameterName, valueInt);
                    break;
                default:
                    break;
            }
        }
    }
}