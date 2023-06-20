using System;
using Core.Locations.Model;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Chapters
{
    [Serializable]
    public class Chapter
    {
        public string ChapterName => chapterName;

#if UNITY_WEBGL
        public bool StartAR => startAR;
#else
        public ScreenOrientation ScreenOrientation => screenOrientation;
#endif


        [SerializeField, HideInInspector] private string chapterName;
        public LocationSetting[] locationSettings;
#if UNITY_WEBGL
        [SerializeField] private bool startAR;
#else
        [SerializeField] private ScreenOrientation screenOrientation;
#endif

        public void OnValidate()
        {
            const string none = "NONE";
            foreach (var setting in locationSettings)
                chapterName += $"{(setting ? setting.name : none)}~";
        }
    }
}