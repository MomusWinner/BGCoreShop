﻿using Core.ObjectsSystem;
using UnityEngine;

namespace Game.UI
{
    public interface IUiElement : IDroppable
    {
        GameObject Root { get; }
        bool IsShown { get; }
        void Show();
        void Hide();
        void Update<TUiAgs>(object sender, TUiAgs ags);
    }
}