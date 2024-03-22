using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;

[MainToolbarElement(group: "Left Test Group")]
public class TestGroupButton : MainToolbarButton
{
    public TestGroupButton() : base("Test Group Button", () => Debug.Log("Test group button"))
    {
    }
}
