using System;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal class MainToolbarAutomaticExtenderOptionsButton : EditorToolbarButton
    {
        private readonly Option[] _options;

        public MainToolbarAutomaticExtenderOptionsButton(params Option[] options)
        {
            _options = options;

            icon = (Texture2D)EditorGUIUtility.IconContent("_Menu@2x").image;
            clicked += ShowOptionsMenu;
            tooltip = "Main Toolbar Automatic Extender options";
        }

        private void ShowOptionsMenu()
        {
            var menu = CreateGenericMenu();

            menu.ShowAsContext();
        }

        private GenericMenu CreateGenericMenu()
        {
            var genericMenu = new GenericMenu();

            foreach(var option in _options)
            {
                genericMenu.AddItem(new GUIContent(option.Text), false, () => option.Action?.Invoke());
            }

            return genericMenu;
        }

        public struct Option
        {
            public string Text;
            public Action Action;
        }
    }
}