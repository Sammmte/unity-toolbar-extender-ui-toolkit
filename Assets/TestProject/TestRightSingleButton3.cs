using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;

[MainToolbarElement(align: ToolbarAlign.Right)]
public class TestRightSingleButton3 : MainToolbarButton
{
    public TestRightSingleButton3() : base("Test Single Button", () => Debug.Log("Test single button"))
    {
    }
}