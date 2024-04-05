using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [InitializeOnLoad]
    public static class MainToolbar
    {
        private const string TOOLBAR_ROOT_ELEMENT_FIELD_NAME = "m_Root";
        private const string TOOLBAR_CENTER_CONTAINER_NAME = "ToolbarZonePlayMode";
        private const string TOOLBAR_LEFT_CONTAINER_NAME = "ToolbarZoneLeftAlign";
        private const string TOOLBAR_RIGHT_CONTAINER_NAME = "ToolbarZoneRightAlign";
        private const string TOOLBAR_PLAY_BUTTON_NAME = "Play";

        private static Type _toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
        private static ScriptableObject _innerToolbarObject;

        public static event Action OnInitialized;
        public static event Action OnRefresh;

        public static VisualElement UnityToolbarRoot { get; private set; }

        public static VisualElement LeftContainer { get; private set; }
        public static VisualElement CenterContainer { get; private set; }
        public static VisualElement RightContainer { get; private set; }
        public static VisualElement PlayModeButtonsContainer { get; private set; }

        public static bool IsAvailable => _innerToolbarObject != null;

        private static bool _initialized;

        static MainToolbar()
        {
            EditorApplication.update -= OnUpdate;
            EditorApplication.update += OnUpdate;
        }

        private static void WrapNativeToolbar()
        {
            FindUnityToolbar();
            if (_innerToolbarObject == null)
                return;
            CacheNativeToolbarContainers();

            if(!_initialized)
            {
                _initialized = true;
                OnInitialized?.Invoke();
            }
            else
                OnRefresh?.Invoke();
        }

        private static void FindUnityToolbar()
        {
            var toolbars = Resources.FindObjectsOfTypeAll(_toolbarType);
            _innerToolbarObject = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
        }

        private static void CacheNativeToolbarContainers()
        {
            var unityToolbarRootFieldInfo = _innerToolbarObject.GetType()
                .GetField(TOOLBAR_ROOT_ELEMENT_FIELD_NAME, BindingFlags.NonPublic | BindingFlags.Instance);
            UnityToolbarRoot = unityToolbarRootFieldInfo.GetValue(_innerToolbarObject) as VisualElement;

            LeftContainer = UnityToolbarRoot.Q(TOOLBAR_LEFT_CONTAINER_NAME);
            CenterContainer = UnityToolbarRoot.Q(TOOLBAR_CENTER_CONTAINER_NAME);
            RightContainer = UnityToolbarRoot.Q(TOOLBAR_RIGHT_CONTAINER_NAME);
            PlayModeButtonsContainer = CenterContainer.Q(TOOLBAR_PLAY_BUTTON_NAME).parent;
        }

        private static void OnUpdate()
        {
            if (NeedsWrap())
            {
                WrapNativeToolbar();
            }
        }

        private static bool NeedsWrap()
        {
            return _innerToolbarObject == null;
        }
    }
}