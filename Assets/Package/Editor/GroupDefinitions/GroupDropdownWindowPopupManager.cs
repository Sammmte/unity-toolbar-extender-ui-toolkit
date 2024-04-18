using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using PopupWindow = UnityEditor.PopupWindow;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class GroupDropdownWindowPopupManager
    {
        private const string POPUP_WINDOW_CONTENT_FIELD_NAME = "m_WindowContent";

        private static FieldInfo _popupWindowContentField;
        private static Type[] _subWindowTypes = new Type[0];
        private static List<GroupDropdownWindowPopup> _windows = new List<GroupDropdownWindowPopup>();

        static GroupDropdownWindowPopupManager()
        {
            _popupWindowContentField = typeof(PopupWindow)
                    .GetField(POPUP_WINDOW_CONTENT_FIELD_NAME,
                    BindingFlags.NonPublic | BindingFlags.Instance);
            LoadSubWindowTypes();
            EditorApplication.update += Update;
        }

        private static void LoadSubWindowTypes()
        {
            _subWindowTypes = TypeCache.GetTypesWithAttribute<GroupPopupSubWindowAttribute>()
                            .Where(type => IsValidSubWindowType(type))
                            .ToArray();
        }

        private static bool IsValidSubWindowType(Type type)
        {
            return typeof(EditorWindow).IsAssignableFrom(type) ||
                typeof(PopupWindowContent).IsAssignableFrom(type);
        }

        private static bool ContainsValidPopupWindowContent(EditorWindow window)
        {
            if(window is PopupWindow popupWindow)
            {
                var popupContent = _popupWindowContentField.GetValue(popupWindow) as PopupWindowContent;
                
                var popupContentSpecificType = popupContent.GetType();

                return _subWindowTypes.Contains(popupContentSpecificType);
            }

            return false;
        }

        private static void Update()
        {
            if (_windows.Count == 0)
                return;

            if (EditorWindow.focusedWindow == null || !FocusedWindowIsValid())
                CloseAll();
            else if(_windows.Contains(EditorWindow.focusedWindow))
            {
                var focusedWindow = EditorWindow.focusedWindow;

                var downMostWindow = _windows.Last();

                while(downMostWindow != focusedWindow)
                {
                    _windows.Remove(downMostWindow);
                    downMostWindow.Close();
                    downMostWindow = _windows.Last();
                }
            }
        }

        private static void CloseAll()
        {
            foreach(var window in _windows)
            {
                if(window != null)
                    window.Close();
            }

            _windows.Clear();
        }

        private static bool FocusedWindowIsValid()
        {
            return _windows.Contains(EditorWindow.focusedWindow) || 
                _subWindowTypes.Contains(EditorWindow.focusedWindow.GetType()) ||
                IsUIToolkitDebugger(EditorWindow.focusedWindow) ||
                ContainsValidPopupWindowContent(EditorWindow.focusedWindow);
        }

        private static bool IsUIToolkitDebugger(EditorWindow window)
        {
            var uiToolkitDebuggerType = TypeCache.GetTypesDerivedFrom<EditorWindow>()
                .FirstOrDefault(type => type.Name == "UIElementsDebugger");

            if (uiToolkitDebuggerType == null)
                return false;

            return uiToolkitDebuggerType == window.GetType();
        }

        public static void Show(Rect activatorRect, VisualElement[] elements)
        {
            var window = ScriptableObject.CreateInstance<GroupDropdownWindowPopup>();
            
            var rect = GUIUtility.GUIToScreenRect(activatorRect);
            rect.y += activatorRect.size.y;
            window.position = new Rect(rect.position, window.position.size);

            _windows.Add(window);

            window.Initialize(elements);
            window.ShowPopup();
        }
    }
}
