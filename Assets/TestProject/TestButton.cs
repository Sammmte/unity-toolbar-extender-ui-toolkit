using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;

[MainToolbarElement]
public class TestButton : EditorToolbarButton
{
    public TestButton() : base(() => Debug.Log("This is a test"))
    {
        text = "Test Button";
    }
}
