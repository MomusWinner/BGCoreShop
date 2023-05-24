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
        public LocationSetting StaticLocationSetting => staticSetting;
        public LocationSetting DynamicLocationSetting => dynamicSetting;

#if UNITY_WEBGL
        public bool StartAR => startAR;
#else
        public ScreenOrientation ScreenOrientation => screenOrientation;
#endif


        [SerializeField, HideInInspector] private string chapterName;
        [SerializeField] private LocationSetting staticSetting;
        [SerializeField] private LocationSetting dynamicSetting;
#if UNITY_WEBGL
        [SerializeField] private bool startAR;
#else
        [SerializeField] private ScreenOrientation screenOrientation;
#endif

        public void OnValidate()
        {
            const string none = "NONE";
            chapterName = $"{(staticSetting ? staticSetting.name : none)}~" +
                          $"{(dynamicSetting ? dynamicSetting.name : none)}";
        }
    }
}