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

        [SerializeField, HideInInspector] private string chapterName;
        [FormerlySerializedAs("statLocationSetting")] [SerializeField] private LocationSetting staticSetting;
        [FormerlySerializedAs("dynamicLocationSetting")] [SerializeField] private LocationSetting dynamicSetting;

        public void OnValidate()
        {
            const string none = "NONE";
            chapterName = $"{(staticSetting ? staticSetting.name : none)}~" +
                          $"{(dynamicSetting ? dynamicSetting.name : none)}";
        }
    }
}