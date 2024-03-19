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

        private static Type _toolbarType = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
        private static ScriptableObject _innerToolbarObject;

        private static event Action OnInitialized;

        public static VisualElement LeftContainer { get; private set; }
        public static VisualElement CenterContainer { get; private set; }
        public static VisualElement RightContainer { get; private set; }

        public static bool IsInitialized { get; private set; }

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

            var wasInitialized = IsInitialized;
            IsInitialized = true;

            if (!wasInitialized)
            {
                OnInitialized?.Invoke();
                OnInitialized = null;
            }
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
            var unityToolbarRoot = unityToolbarRootFieldInfo.GetValue(_innerToolbarObject) as VisualElement;

            LeftContainer = unityToolbarRoot.Q(TOOLBAR_LEFT_CONTAINER_NAME);
            CenterContainer = unityToolbarRoot.Q(TOOLBAR_CENTER_CONTAINER_NAME);
            RightContainer = unityToolbarRoot.Q(TOOLBAR_RIGHT_CONTAINER_NAME);
        }

        private static void OnUpdate()
        {
            if (_innerToolbarObject == null)
            {
                Build();
            }
        }

        public static void ExecuteIfOrWhenIsInitialized(Action action)
        {
            if (IsInitialized)
                action?.Invoke();
            else
                OnInitialized += action;
        }
    }
}