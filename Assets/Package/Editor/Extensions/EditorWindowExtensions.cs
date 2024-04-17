using UnityEditor;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    public static class EditorWindowExtensions
    {
        public static void ShowAsDropdownForMainToolbar(this EditorWindow window, Rect activatorRect, Vector2 size)
        {
            window.ShowAsDropDown(activatorRect, size);

            var rect = GUIUtility.GUIToScreenRect(activatorRect);
            rect.y += activatorRect.size.y;
            window.position = new Rect(rect.position, window.position.size);
        }
    }
}
