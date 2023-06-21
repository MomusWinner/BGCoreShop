namespace Game.Settings.UISettings
{
    public abstract class UISetting : ViewSetting
    {
        public bool showOnAlive = true;
        public bool showHideChildAffect = true;
        public UISetting[] childUiElementSettings;
    }
}