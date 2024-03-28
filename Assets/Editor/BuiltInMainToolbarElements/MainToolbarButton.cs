using System;
using UnityEditor.Toolbars;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarButton : EditorToolbarButton
    {
        public MainToolbarButton(string label, Action onClick) : base(label, onClick)
        {
        }

        public MainToolbarButton(Texture2D icon, Action onClick) : base(icon, onClick)
        {

        }

        public MainToolbarButton() : base()
        {

        }
    }
}