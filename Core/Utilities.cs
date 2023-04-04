using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public static class Utilities
    {
        public static string ToShortString(this object value)
        {
            var str = (string) value;
            return float.TryParse(str, out var number) ? ToShortString(number) : value.ToString();
        }

        public static string ToShortString(this float value)
        {
            var mag = (int) (System.Math.Round(System.Math.Log10(value)) / 3);
            double divisor = Mathf.Pow(10, mag * 3);

            var shortNumber = value / divisor;

            var suffix = "";
            var format = "N0";
            switch (mag)
            {
                case 0:
                    suffix = "";
                    break;
                case 1:
                    suffix = "k";
                    format = "N1";
                    break;
                case 2:
                    suffix = "M";
                    format = "N1";
                    break;
                case 3:
                    suffix = "B";
                    format = "N1";
                    break;
            }

            return shortNumber.ToString(format) + suffix;
        }
#if UNITY_EDITOR
        public static string GetValidPathToResource(Object resourcesObject)
        {
            var path = AssetDatabase.GetAssetPath(resourcesObject);
            path = path[(path.IndexOf("Resources", StringComparison.Ordinal) + 10)..];
            return path.Split('.')[0];
        }
#endif
        public static string TimeFromSeconds(float time)
        {
            int minutes = (int) time / 60;
            int seconds = (int) time - 60 * minutes;
            int milliseconds = (int) (1000 * (time - minutes * 60 - seconds));
            return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }

        public static bool TryParseVector2(string value, out Vector2 vector2)
        {
            var temp=value.Split(',');
            
            vector2 = default;
            if (!float.TryParse(temp[0], out var x) || !float.TryParse(temp[1], out var y))
                return false;

            vector2 = new Vector2(x, y);
            return true;
        }
    }

    public abstract class KeyValuePair<TKey, TValue>
    {
        public TKey Key => key;
        public TValue Value => value;

        [SerializeField, HideInInspector] public string name;
        [SerializeField] protected TKey key;
        [SerializeField] protected TValue value;
    }

    public static class HashCode
    {
        public static int Combine(int a, int b)
        {
            return ((a << 5) + a) ^ b;
        }
    }
}