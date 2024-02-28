namespace Game.Settings.UISettings
{
    public abstract class UISetting : ViewSetting
    {
        public bool showOnAlive = true;
        public UISetting[] childUiElementSettings;
    }
}