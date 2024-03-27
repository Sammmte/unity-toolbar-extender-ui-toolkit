using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [InitializeOnLoad]
    public static class ToolbarWrapper
    {
        private const string TOOLBAR_ROOT_ELEMENT_FIELD_NAME = "m_Root";
        private const string TOOLBAR_CENTER_CONTAINER_NAME = "ToolbarZonePlayMode";
        private const string TOOLBAR_LEFT_CONTAINER_NAME = "ToolbarZoneLeftAlign";
        private const string TOOLBAR_RIGHT_CONTAINER_NAME = "ToolbarZoneRightAlign";
        private const string TOOLBAR_PLAY_BUTTON_NAME = "Play";

        private static Type _toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
        private static ScriptableObject _innerToolbarObject;

        public static event Action OnNativeToolbarWrapped;

        public static VisualElement UnityToolbarRoot { get; private set; }

        public static VisualElement LeftContainer { get; private set; }
        public static VisualElement CenterContainer { get; private set; }
        public static VisualElement RightContainer { get; private set; }
        public static VisualElement PlayModeButtonsContainer { get; private set; }

        public static bool IsAvailable => _innerToolbarObject != null;

        static ToolbarWrapper()
        {
            EditorApplication.update -= OnUpdate;
            EditorApplication.update += OnUpdate;
        }

        private static void Build()
        {
            FindUnityToolbar();
            if (_innerToolbarObject == null)
                return;
            AddToolbarSpacesToNativeToolbar();

            OnNativeToolbarWrapped?.Invoke();
        }

        private static void FindUnityToolbar()
        {
            var toolbars = Resources.FindObjectsOfTypeAll(_toolbarType);
            _innerToolbarObject = toolbars.Length > 0 ? (ScriptableObject)toolbars[0] : null;
        }

        private static void AddToolbarSpacesToNativeToolbar()
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
            if (NeedsRebuild())
            {
                Build();
            }
        }

        private static bool NeedsRebuild()
        {
            return _innerToolbarObject == null;
        }
    }
}