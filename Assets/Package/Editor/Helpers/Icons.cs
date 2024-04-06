using System;
using UnityEditor;
using UnityEngine;

namespace Paps.UnityToolbarExtenderUIToolkit
{
    internal static class Icons
    {
        private static readonly Lazy<Texture> VISIBILITY_ON_OVERRIDE_ICON_LAZY = 
            new Lazy<Texture>(() => EditorGUIUtility.IconContent("animationvisibilitytoggleon").image);

        private static readonly Lazy<Texture> VISIBILITY_OFF_OVERRIDE_ICON_LAZY =
            new Lazy<Texture>(() => EditorGUIUtility.IconContent("animationvisibilitytoggleoff").image);

        public static Texture VisibilityOnOverrideIcon => VISIBILITY_ON_OVERRIDE_ICON_LAZY.Value;
        public static Texture VisibilityOffOverrideIcon => VISIBILITY_OFF_OVERRIDE_ICON_LAZY.Value;
    }
}