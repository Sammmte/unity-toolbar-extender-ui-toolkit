using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class GroupDropdownWindowPopupManager
    {
        private static List<GroupDropdownWindowPopup> _windows = new List<GroupDropdownWindowPopup>();

        static GroupDropdownWindowPopupManager()
        {
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            if (_windows.Count == 0)
                return;

            if (EditorWindow.focusedWindow == null || !FocusedWindowIsValid())
                CloseAll();
            else
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
            return _windows.Contains(EditorWindow.focusedWindow);
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
