using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement]
public class TestSingleButton : EditorToolbarButton
{
    public TestSingleButton() : base(() => Debug.Log("Test single button"))
    {
        text = "Test Button";
    }
}