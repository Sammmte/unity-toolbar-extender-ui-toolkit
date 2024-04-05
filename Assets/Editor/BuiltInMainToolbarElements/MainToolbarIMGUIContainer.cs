using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public class MainToolbarIMGUIContainer : IMGUIContainer
    {
        private Action _onGui;

        new public Action onGUIHandler
        {
            get => _onGui;
            set => _onGui = value;
        }

        public MainToolbarIMGUIContainer(Action onGui)
        {
            _onGui = onGui;
            base.onGUIHandler = OnGUI;
        }

        public MainToolbarIMGUIContainer() : this(null)
        {

        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            _onGui?.Invoke();
            GUILayout.EndHorizontal();
        }
    }
}