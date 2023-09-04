using System.Linq;
using Core;
using Core.Locations.Model;
using Core.Locations.View;
using Core.LoopSystem;
using Core.ObjectsSystem;
using Core.Timers;
using Game.Contexts;
using Game.LocationObjects;
using Game.UI;
using GameData;
using UnityEngine.SceneManagement;

namespace Game.Locations
{
    public class SceneLocation : Location
    {
        public Scene Scene { get; private set; }

        private readonly IUiElement fade;
        private ITimer switchChapterDelayTimer;
        

        public SceneLocation(LocationSetting setting, IContext ctx, IDroppable parent) : base(setting, ctx, parent)
        {
            CurrentAliveChild = 0;
            if (setting.startFade)
                fade = (IUiElement) setting.startFade.GetInstance(ctx, this);

            for (var i = 0; i < setting.childSettings.Length; i++)
            {
                var droppable = setting.childSettings[i].GetInstance(ctx, this);
                (droppable as ILocationObject)?.SetLoadOrder(i);
                droppable.OnLively += DroppableOnOnLively;
                droppables.Add(droppable);
            }

            Scene = SceneManager.GetSceneByName(setting.sceneName);

            if (Scene is {isLoaded: true})
            {
                CreateLocationView(setting);
                return;
            }

            var operation = SceneManager.LoadSceneAsync(setting.sceneName, LoadSceneMode.Additive);
            operation.completed += _ =>
            {
                Scene = SceneManager.GetSceneByName(setting.sceneName);
                CreateLocationView(setting);
            };
        }

        private void CreateLocationView(LocationSetting setting)
        {
            view = (LocationView) setting.GetViewInstance(context, this);
            switchChapterDelayTimer = TimerFactory.CreateTimer(Loops.Update, setting.fadeDelay, UnFade, false);
            switchChapterDelayTimer.SetAlive();
            fade?.Show(OnFaded);
        }

        private void DroppableOnOnLively(IDroppable droppable)
        {
            droppable.OnLively -= DroppableOnOnLively;
            if(droppable is IUiElement element)
                Scheduler.InvokeWhen(() => UIAlive(element), () =>
                {
                    CurrentAliveChild++;
                });
            else
            {
                CurrentAliveChild++;
            }
        }

        private bool UIAlive(IUiElement element)
        {
            return element.ChildUiElements is { } &&
                   (element.ChildUiElements.Count == 0 || element.ChildUiElements.All(c => c.Alive && UIAlive(c)));
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

        private void OnFaded(object value)
        {
            WebGLBridge.EngineStarted();
            Scheduler.InvokeWhen(() => Alive && droppables.All(d => d is {Alive: true}), () => switchChapterDelayTimer.Play());
        }

        private void UnFade(object obj)
        {
            switchChapterDelayTimer.Stop();
            fade?.Hide(OnUnFaded);
        }
        
        private void OnUnFaded(object obj)
        {
            fade?.Drop();
            //fade = new Fade(changeSectionFade, null);
        }

    }
}