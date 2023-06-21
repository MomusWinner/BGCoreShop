using System.Linq;
using Core;
using Core.Locations.Model;
using Core.Locations.View;
using Core.LoopSystem;
using Core.ObjectsSystem;
using Core.Timers;
using Game.Contexts;
using Game.UI;
using GameData;
using UnityEngine.SceneManagement;

namespace Game.Locations
{
    public class SceneLocation : Location
    {
        public Scene Scene { get; private set; }

        private IUiElement fade;
        private ITimer switchChapterDelayTimer;

        public SceneLocation(LocationSetting setting, IContext ctx, IDroppable parent) : base(setting, ctx, parent)
        {
            if (setting.startFade)
                fade = (IUiElement) setting.startFade.GetInstance(ctx, this);

            foreach (var objectsSetting in setting.childSettings)
                droppables.Add(objectsSetting.GetInstance(ctx, this));

            Scene = SceneManager.GetSceneByName(setting.sceneName);

            if (Scene is {isLoaded: true})
            {
                view = (LocationView) setting.GetViewInstance(context, this);
                return;
            }

            var operation = SceneManager.LoadSceneAsync(setting.sceneName, LoadSceneMode.Additive);
            operation.completed += _ =>
            {
                Scene = SceneManager.GetSceneByName(setting.sceneName);
                view = (LocationView) setting.GetViewInstance(context, this);
                switchChapterDelayTimer = TimerFactory.CreateTimer(Loops.Update, setting.fadeDelay, UnFade, false);
                switchChapterDelayTimer.SetAlive();

                void OnFaded(object value)
                {
                    Scheduler.InvokeWhen(() => Alive, () => switchChapterDelayTimer.Play());
                }
                fade?.Show(OnFaded);
            };
        }

        protected override void OnDrop()
        {
            fade?.Drop();
            var chapter = context.GetContext<MainContext>().CurrentChapter;
            var isNewScene = chapter.locationSettings.All(s => s is { } sls && sls.sceneName != Scene.name);
            if (isNewScene && Scene.isLoaded)
                SceneManager.UnloadSceneAsync(Scene.name);
            base.OnDrop();
        }
        
        private void UnFade(object obj)
        {
            switchChapterDelayTimer.Stop();
            fade?.Hide(OnUnFaded);
        }
        
        private void OnUnFaded(object obj)
        {
            //fade?.Drop();
            //fade = new Fade(changeSectionFade, null);
        }

    }
}