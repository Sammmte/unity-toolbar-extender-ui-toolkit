using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;

[MainToolbarElement(group: TestGroups.LeftGroup)]
public class TestLeftGroupButton : MainToolbarButton
{
    public TestLeftGroupButton() : base("Test Group Button", () => Debug.Log("Test group button"))
    {
    }
}
