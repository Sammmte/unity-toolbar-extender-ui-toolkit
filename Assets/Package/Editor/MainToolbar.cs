using System;
using System.Linq;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    [InitializeOnLoad]
    internal static class MainToolbar
    {
        private static Type _toolbarWindowType = typeof(Editor).Assembly.GetType("UnityEditor.MainToolbarWindow");
        private static EditorWindow _toolbarWindow;
        private static int _lastOverlayToolbarCount;

        public static event Action OnInitialized;
        public static event Action OnRefresh;

        public static bool IsAvailable => _toolbarWindow != null;

        private static bool _initialized;

        static MainToolbar()
        {
            EditorApplication.update -= OnUpdate;
            EditorApplication.update += OnUpdate;
        }

        private static void WrapNativeToolbar()
        {
            FindUnityToolbar();
            if (_toolbarWindow == null)
                return;

            if(!_initialized)
            {
                _initialized = true;
                OnInitialized?.Invoke();
            }
            else
            {
                OnRefresh?.Invoke();
            }
        }

        private static void FindUnityToolbar()
        {
            _toolbarWindow = EditorWindow.GetWindow(_toolbarWindowType);
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
            if (_toolbarWindow == null)
                return true;

            var query = _toolbarWindow.rootVisualElement.Query<OverlayToolbar>();

            if (query.First() != null)
            {
                var currentOverlayToolbarCount = query.ToList().Count;

                if (currentOverlayToolbarCount != _lastOverlayToolbarCount)
                {
                    _lastOverlayToolbarCount = currentOverlayToolbarCount;
                    return true;
                }
            }

            return false;
        }

        public static bool TryClearDummyToolbarElement(string id)
        {
            var mainContainerElement = _toolbarWindow.rootVisualElement.Q(id);

            if (mainContainerElement == null)
                return false;
            
            var overlayToolbarElement = mainContainerElement.Q<OverlayToolbar>();

            if (overlayToolbarElement == null)
                return false;

            var children = overlayToolbarElement.Children().ToArray();
            
            foreach (VisualElement child in children)
            {
                overlayToolbarElement.Remove(child);
            }

            return true;
        }

        public static bool TryReplace(MainToolbarElement element)
        {
            var mainContainerElement = _toolbarWindow.rootVisualElement.Q(element.Id);

            if (mainContainerElement == null)
                return false;
            
            var overlayToolbarElement = mainContainerElement.Q<OverlayToolbar>();

            if (overlayToolbarElement == null)
                return false;

            var children = overlayToolbarElement.Children().ToArray();
            
            foreach (VisualElement child in children)
            {
                overlayToolbarElement.Remove(child);
            }
            
            overlayToolbarElement.Add(element.VisualElement);

            return true;
        }

        public static bool CanBeReplaced(MainToolbarElement element)
        {
            var mainContainerElement = _toolbarWindow.rootVisualElement.Q(element.Id);

            return mainContainerElement != null;
        }
    }
}