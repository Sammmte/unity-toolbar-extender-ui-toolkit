using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[InitializeOnLoad]
public static class ToolbarTest
{
    static ToolbarTest()
    {
        var someButton = new EditorToolbarButton(() => Debug.Log("XOXO")) {
            text = "HOLO"
        };
        var someLabel = new Label("SOME LABEL");
        //ToolbarWrapper.RightCustomContainer.Add(someButton);
        //ToolbarWrapper.LeftCustomContainer.Add(someLabel);
    }
}
