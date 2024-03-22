using Paps.UnityToolbarExtenderUIToolkit;
using UnityEngine;

[MainToolbarElement(align: ToolbarAlign.Right)]
public class TestRightSingleButton2 : MainToolbarButton
{
    public TestRightSingleButton2() : base("Test Single Button", () => Debug.Log("Test single button"))
    {
    }
}
