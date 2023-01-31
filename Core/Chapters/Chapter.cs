using System;
using Core.Locations.Model;
using UnityEngine;

namespace Core.Chapters
{
    [Serializable]
    public class Chapter
    {
        public string ChapterName => chapterName;
        public LocationSetting[] LocationSetting => locationSettings;

        [SerializeField, HideInInspector] private string chapterName;
        [SerializeField] private LocationSetting[] locationSettings;
        
        public void OnValidate()
        {
            const string none = "NONE";
            chapterName = string.Empty;
            if(locationSettings is null)
                return;
            foreach (var locationSetting in locationSettings)
            {
                chapterName += $"{(locationSetting ? locationSetting.name : none)}~";
            }
        }
    }
}