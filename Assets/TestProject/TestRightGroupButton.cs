using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;

[MainToolbarElement(align: ToolbarAlign.Right, group: TestGroups.RightGroup)]
public class TestRightGroupButton : MainToolbarButton
{
    public TestRightGroupButton() : base("Test Group Button", () => Debug.Log("Test group button"))
    {
    }
}
