using Core;
using Core.Chapters;
using Core.Locations.Model;
using Game.GameData;
using GameLogic.GameData;
using UnityEngine;

namespace GameLogic
{
    public class ChapterContainer : MonoBehaviour
    {
        [SerializeField] private Chapter[] chapters;

        public static LocationSection ActiveSection { get; private set; }


        private void Start()
        {
            StartChapter(chapters[0], new Context(1, "1"));
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            foreach (var chapter in chapters)
            {
                chapter?.OnValidate();
            }
        }
#endif

        public void StartChapter(Chapter chapter, BaseContext context)
        {
            if (chapter is null)
            {
                return;
            }

            GEvent.Call(GlobalEvents.DropSection);

            ActiveSection = new LocationSection(chapter.ChapterName,chapter.StaticLocationSetting,
                                                                    chapter.DynamicLocationSetting,
                                                context);

            GEvent.Call(GlobalEvents.Start);
        }
    }
}