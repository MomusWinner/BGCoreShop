using Core.Main;
using Core.Pool;
using UnityEngine;

namespace Core.GameData.DataContainers
{
    public class DataBase : Singleton<DataBase>
    {
        public PoolNote[] PoolNotes => poolNotes;
        [SerializeField] private PoolNote[] poolNotes;

#if UNITY_EDITOR
        private void OnValidate()
        {
            PoolManager.OnValidate(poolNotes);
        }
#endif
    }
}