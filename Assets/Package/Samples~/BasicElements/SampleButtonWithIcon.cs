using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using MainToolbarElement = Paps.UnityToolbarExtenderUIToolkit.MainToolbarElementAttribute;

[MainToolbarElement(id: "SampleButtonWithIcon", order: 2)]
public class SampleButtonWithIcon : EditorToolbarButton
{
    public void InitializeElement()
    {
        icon = (Texture2D)EditorGUIUtility.IconContent("_Popup@2x").image;
        tooltip = "This is an example of a toolbar button with an icon";
        clicked += () => Debug.Log("A debug log from sample button with an icon");
    }
}