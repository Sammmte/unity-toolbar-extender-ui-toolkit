using Paps.UnityToolbarExtenderUIToolkit;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.UIElements;

[MainToolbarElement(align: ToolbarAlign.Right)]
public class TestRightSingleButton : MainToolbarButton
{
    public TestRightSingleButton() : base("Test Single Button", () => Debug.Log("Test single button"))
    {
    }
}