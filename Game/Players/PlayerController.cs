using Core;
using Core.LoopSystem;
using Game.Characters.Control;
using GameLogic.Players;
using UnityEngine;
using Views;

namespace GameLogic
{
    public class PlayerController : BaseCharacterControl
    {
        private readonly PlayerComponent playerComponent;

        private readonly PlayerSetting playerSetting;

        private readonly PlayerControllerView view;
        

        public PlayerController(PlayerSetting setting, Transform transform) : base(transform.name)
        {
            playerComponent = transform.GetComponentInChildren<PlayerComponent>();
            playerSetting = setting;

            EnableActions(setting);

            view = new PlayerControllerView(setting, "[Controller] " + transform.name);
            view.OnRestartClick += OnRestartClick;

            view.SetAlive();
            
            LoopOn(Loops.FixedUpdate, OnUpdate);
        }

        private static void OnRestartClick()
        {
            GEvent.Call(GlobalEvents.Restart, ChapterContainer.ActiveSection.DynLocation);
        }

        private void EnableActions(PlayerSetting setting)
        {
        }

        protected override void OnDrop()
        {
            base.OnDrop();

            view?.Drop();
        }

        private void OnUpdate()
        {
        }
    }
}