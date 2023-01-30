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
        public ScreenOrientation ScreenOrientation => screenOrientation;

        [SerializeField, HideInInspector] private string chapterName;
        [SerializeField] private LocationSetting staticSetting;
        [SerializeField] private LocationSetting dynamicSetting;
        [SerializeField] private ScreenOrientation screenOrientation;

        public void OnValidate()
        {
            const string none = "NONE";
            chapterName = $"{(staticSetting ? staticSetting.name : none)}~" +
                          $"{(dynamicSetting ? dynamicSetting.name : none)}";
        }
    }
}