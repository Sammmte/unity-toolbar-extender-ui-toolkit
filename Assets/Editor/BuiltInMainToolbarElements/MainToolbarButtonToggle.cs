using UnityEditor.Toolbars;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarButtonToggle : EditorToolbarToggle
    {
        public MainToolbarButtonToggle() : base()
        {

        }

        public MainToolbarButtonToggle(string text) : base(text)
        {

        }

        public MainToolbarButtonToggle(Texture2D icon) : base(icon)
        {

        }

        public MainToolbarButtonToggle(Texture2D iconOn, Texture2D iconOff) : base(iconOn, iconOff)
        {

        }

        public MainToolbarButtonToggle(string text, Texture2D iconOn, Texture2D iconOff) : base(text, iconOn, iconOff)
        {

        }
    }
}