using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;

[MainToolbarElement(group: "Left Test Group")]
public class TestGroupButton : EditorToolbarButton
{
    public TestGroupButton() : base(() => Debug.Log("Test group button"))
    {
        text = "Test Button";
    }
}
