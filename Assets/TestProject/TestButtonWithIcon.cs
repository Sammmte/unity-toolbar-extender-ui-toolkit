using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;

[MainToolbarElement(nameof(TestButtonWithIcon))]
public class TestButtonWithIcon : EditorToolbarButton
{
    public TestButtonWithIcon() : base((Texture2D)EditorGUIUtility.IconContent("_Popup@2x").image, () => Debug.Log("I have an icon"))
    {
    }
}
