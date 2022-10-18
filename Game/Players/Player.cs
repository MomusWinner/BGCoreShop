using Core.Locations.Model;
using Game.Characters.Model;
using Game.GameData;

namespace GameLogic
{
    public class Player : BaseCharacter<PlayerView, PlayerController>
    {
        protected sealed override PlayerView View { get; set; }
        protected sealed override PlayerController Control { get; set; }

        private PlayerSetting playerSetting;
        private IContext playerContext;

        public Player(Location parentLocation, string name, PlayerSetting setting, IContext context) : base(parentLocation, name, setting)
        {
            playerSetting = setting;
            playerContext = context;
        }

        protected override void Initialize()
        {
            View = new PlayerView(Name, playerSetting, playerContext, parentLocation.Root.transform);
            View.SetAlive();

            Control = new PlayerController(playerSetting, ViewRoot.transform);
            Control.SetAlive();
            Control.Play();
        }

        protected override void OnDrop()
        {
            base.OnDrop();
            View?.Drop();
            Control?.Drop();
            Control = null;
            View = null;
            Control = null;
        }
    }
}